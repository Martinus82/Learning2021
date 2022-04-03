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

        public void StartCharging(SlotId slotId, bool requestTurboCharging)
        {
            var power = _powerDistributor.RequestPower(requestTurboCharging);
            _slots[slotId].StartCharging(power, requestTurboCharging);
        }

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

        public void StartCharging(SlotId slotId)
        {
            Slot slot = _slots[slotId];
            var power = _powerDistributor.RequestPower(false);
            slot.StartCharging(power);
        }

        public void StopCharging(SlotId slotId)
        {
            Slot slot = _slots[slotId];
            slot.StopCharging();
            _powerDistributor.RedistributeReleasedPowerToTurboSlots();
        }

        public SlotState GetSlotState(SlotId slotId)
        {
            Slot slot = _slots[slotId];
            return slot.State;
        }
    }
}
