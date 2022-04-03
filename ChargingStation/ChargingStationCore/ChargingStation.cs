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
            _powerDistributor = new PowerDistributor(_slots.Select(s => s.Value.SlotState));
        }

        public ChargingState ChargingState =>
            _slots.Values.Any(slot => slot.SlotState.ChargingState == ChargingState.Charging)
                ? ChargingState.Charging
                : ChargingState.NonCharging;

        public int Power => _slots.Values.Sum(s => s.SlotState.Power);

        public void StartCharging(SlotId slotId, bool turboChargingRequested)
        {
            int power = _powerDistributor.RequestPower(turboChargingRequested);
            if (power == 0)
            {
                power = _powerDistributor.ComputePowerDecreasePerTurboSlot(turboChargingRequested);
                RedistributePowerPerEachActiveTurboSlot(power);
            }

            _slots[slotId].StartCharging(power, turboChargingRequested);
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

        public SlotState GetSlotState(SlotId slotId) => _slots[slotId].SlotState;

        private void RedistributePowerPerEachActiveTurboSlot(int newTurboPower)
        {
            var activeTurboSlots = _slots
                .Select(s => s.Value)
                .Where(s => s.SlotState.IsTurboChargingEnabled && s.SlotState.ChargingState == ChargingState.Charging)
                .ToList();

            activeTurboSlots.ForEach(slot => slot.UpdatePowerDrain(newTurboPower));
        }
    }
}
