using System;
using System.Collections.Generic;
using System.Linq;

namespace ChargingStationCore
{
    internal class PowerDistributor
    {
        private const int MaxTotalStationChargingPower = 400;
        private readonly IEnumerable<SlotState> _slotStates;

        public PowerDistributor(IEnumerable<SlotState> slotStates)
        {
            _slotStates = slotStates ?? throw new ArgumentNullException(nameof(slotStates));
        }

        public int GetAvailablePower(bool turboChargingRequested)
        {
            int availablePower = GetAvailablePower();
            int requestedPower = turboChargingRequested ? Slot.TurboPower : Slot.DefaultPower;
            if (availablePower >= requestedPower)
            {
                return requestedPower;
            }

            return 0;
        }

        public int ComputePowerDecreasePerTurboSlot(bool turboRequested)
        {
            var activeTurboChargingSlots = _slotStates.Where(s => s.IsTurboChargingEnabled == true && s.ChargingState == ChargingState.Charging);
            var activeNonTurboChargingSlots = _slotStates.Where(s => s.IsTurboChargingEnabled == false && s.ChargingState == ChargingState.Charging);

            int totalTurboPowerToBeRedistributed;

            if (turboRequested)
            {
                totalTurboPowerToBeRedistributed = MaxTotalStationChargingPower - activeNonTurboChargingSlots.Sum(n => n.Power);
                const int oneMoreTurboSlotRequested = 1;
                return totalTurboPowerToBeRedistributed / (activeTurboChargingSlots.Count() + oneMoreTurboSlotRequested);
            }

            totalTurboPowerToBeRedistributed = MaxTotalStationChargingPower - Slot.DefaultPower - activeNonTurboChargingSlots.Sum(n => n.Power);

            if (activeTurboChargingSlots.Count() == 0)
            {
                return totalTurboPowerToBeRedistributed;
            }

            return totalTurboPowerToBeRedistributed / activeTurboChargingSlots.Count();
        }

        public int ComputePowerIncreasePerTurboSlot()
        {
            var activeTurboChargingSlots = _slotStates.Where(s => s.IsTurboChargingEnabled == true && s.ChargingState == ChargingState.Charging);
            var activeNonTurboChargingSlots = _slotStates.Where(s => s.IsTurboChargingEnabled == false && s.ChargingState == ChargingState.Charging);

            int totalTurboPowerToBeRedistributed = MaxTotalStationChargingPower - activeNonTurboChargingSlots.Sum(n => n.Power);

            if (activeTurboChargingSlots.Count() == 0)
            {
                return totalTurboPowerToBeRedistributed;
            }

            return totalTurboPowerToBeRedistributed / activeTurboChargingSlots.Count();
        }

        private int GetAvailablePower()
        {
            int currentPowerDrain = _slotStates.Sum(s => s.Power);
            return MaxTotalStationChargingPower - currentPowerDrain;
        }
    }
}
