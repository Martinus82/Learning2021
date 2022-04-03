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
            _powerDistributor = new PowerDistributor(_slots.Select(s => s.Value.State));
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

        public void StartCharging(SlotId slotId, bool turbo)
        {
            int power = _powerDistributor.GetAvailablePower(turbo);
            if (power == 0)
            {
                power = _powerDistributor.ComputePowerDecreasePerTurboSlot(turbo);
                RedistributePowerPerEachActiveTurboSlot(power);
            }

            _slots[slotId].StartCharging(power, turbo);
        }

        public void StartCharging(SlotId slotId)
        {
            StartCharging(slotId, false);
        }

        public void StopCharging(SlotId slotId)
        {
            _slots[slotId].StopCharging();
            int power = _powerDistributor.ComputePowerIncreasePerTurboSlot();
            RedistributePowerPerEachActiveTurboSlot(power);
        }

        public SlotState GetSlotState(SlotId slotId)
        {
            Slot slot = _slots[slotId];
            return slot.State;
        }

        private void RedistributePowerPerEachActiveTurboSlot(int newTurboPower)
        {
            var activeTurboSlots = _slots
                .Select(s => s.Value)
                .Where(s => s.State.IsTurboChargingSupported && s.State.ChargingState == ChargingState.Charging)
                .ToList();

            activeTurboSlots.ForEach(slot => slot.UpdatePowerDrain(newTurboPower));
        }
    }
}
