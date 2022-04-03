using System;
using System.Linq;
using System.Collections.Generic;

namespace ChargingStationCore
{
    public class ChargingStation
    {
        private readonly Dictionary<SlotId, Slot> _slots = new()
        {
            [SlotId.One] = new Slot(SlotId.One),
            [SlotId.Two] = new Slot(SlotId.Two),
            [SlotId.Three] = new Slot(SlotId.Three),
            [SlotId.Four] = new Slot(SlotId.Four)
        };

        private readonly PowerDistributor _powerDistributor;

        public ChargingStation()
        {
            _powerDistributor = new PowerDistributor(_slots.Values);
        }

        public ChargingState State
        {
            get
            {
                return _slots.Values.Any(x => x.State.ChargingState == ChargingState.Charging)
                    ? ChargingState.Charging
                    : ChargingState.NonCharging;
            }
        }

        public int Power => _slots.Values.Sum(s => s.State.Power);

        public void StartCharging(SlotId slotId, bool requestTurboCharging)
        {
            var power = _powerDistributor.RequestPower(requestTurboCharging);
            bool notEnoughPower = power == 0;
            if (notEnoughPower)
            {
                power = _powerDistributor.ComputePowerDrainDecrease(requestTurboCharging);
                RedistributePowerToActiveTurboSlot(power);
            }

            _slots[slotId].StartCharging(power, requestTurboCharging);
        }

        public void StartCharging(SlotId slotId)
        {
            Slot slot = _slots[slotId];
            var power = _powerDistributor.RequestPower(false);
            bool notEnoughPower = power == 0;
            if (notEnoughPower)
            {
                power = _powerDistributor.ComputePowerDrainDecrease(false);
                RedistributeTotalPowerActiveTurboSlots(power);
            }

            slot.StartCharging(Slot.DefaultPower);
        }

        public void StopCharging(SlotId slotId)
        {
            Slot slot = _slots[slotId];
            slot.StopCharging();
            int totalReleasedPower = _powerDistributor.ComputePowerDrainIncrease();
            RedistributeTotalPowerActiveTurboSlots(totalReleasedPower);
        }

        public SlotState GetSlotState(SlotId slotId)
        {
            Slot slot = _slots[slotId];
            return slot.State;
        }

        public void RedistributePowerToActiveTurboSlot(int powerPerSlot)
        {
            var activeTurboSlots = _slots
                .Select(s => s.Value)
                .Where(s => s.State.IsTurboChargingSupported && s.State.ChargingState == ChargingState.Charging);

            // Because default power cannot be increased, we need to push all the available power only to (between) turbo charging slots.
            foreach (var turboSlot in activeTurboSlots)
            {
                turboSlot.UpdatePowerDrain(powerPerSlot);
            }
        }

        public void RedistributeTotalPowerActiveTurboSlots(int totalPower)
        {
            var activeTurboSlots = _slots
                .Select(s => s.Value)
                .Where(s => s.State.IsTurboChargingSupported && s.State.ChargingState == ChargingState.Charging);

            //int numberOfActiveTurboSlots = activeTurboSlots.Count();

            // Because default power cannot be increased, we need to push all the available power only to (between) turbo charging slots.
            foreach (var turboSlot in activeTurboSlots)
            {
                turboSlot.UpdatePowerDrain(totalPower);
            }
        }
    }
}
