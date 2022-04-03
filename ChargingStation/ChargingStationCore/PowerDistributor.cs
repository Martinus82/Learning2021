using System;
using System.Collections.Generic;
using System.Linq;

namespace ChargingStationCore
{
    internal class PowerDistributor
    {
        private const int MaxTotalStationChargingPower = 400;
        private const int TurboPowerPerSlot = 200;
        private const int DefaultPowerPerSlot = 50;

        private readonly ICollection<Slot> _slots;

        public PowerDistributor(ICollection<Slot> slots)
        {
            _slots = slots ?? throw new ArgumentNullException(nameof(slots));
        }

        public int RequestPower(bool requestTurboCharging)
        {
            int availablePower = GetAvailablePower();
            int requestedPower = requestTurboCharging ? TurboPowerPerSlot : DefaultPowerPerSlot;
            if (availablePower >= requestedPower)
            {
                return requestedPower;
            }

            DecreasePowerDrainOfActiveTurboSlots(requestTurboCharging);

            int newAvailablePower = GetAvailablePower();
            return newAvailablePower;
        }

        public void RedistributeReleasedPowerToTurboSlots()
        {
            var activeTurboChargingSlots = _slots.Where(slot => slot.State.IsTurboChargingSupported == true && slot.State.ChargingState == ChargingState.Charging);
            int currentPowerConsumptionOfTurboSlots = _slots.Where(s => s.State.ChargingState == ChargingState.Charging)
                .Sum(s => s.State.Power);

            int totalPowerAmountWhichCanBeRedistributed = MaxTotalStationChargingPower - currentPowerConsumptionOfTurboSlots;
            int numberOfActiveTurboChargers = activeTurboChargingSlots.Count();

            // Because default power cannot be increased, we need to push all the available power only to (between) turbo charging slots.
            foreach (var turboChargingSlot in activeTurboChargingSlots)
            {
                int powerWhichCanBeRedistributedPerSlot = totalPowerAmountWhichCanBeRedistributed / numberOfActiveTurboChargers;
                int newPower = powerWhichCanBeRedistributedPerSlot + turboChargingSlot.State.Power;
                turboChargingSlot.UpdatePowerDrain(newPower > TurboPowerPerSlot ? TurboPowerPerSlot : newPower);
            }
        }

        private void DecreasePowerDrainOfActiveTurboSlots(bool turboRequested)
        {
            var activeTurboChargingSlots = _slots.Where(slot => slot.State.IsTurboChargingSupported == true && slot.State.ChargingState == ChargingState.Charging);
            var activeNonTurboChargingSlots = _slots.Where(slot => slot.State.IsTurboChargingSupported == false && slot.State.ChargingState == ChargingState.Charging);

            if (turboRequested)
            {
                foreach (var turboChargingSlot in activeTurboChargingSlots)
                {
                    int totalAvailableTurboPower = MaxTotalStationChargingPower - activeNonTurboChargingSlots.Sum(n => n.State.Power);
                    int newTurboPower = totalAvailableTurboPower / (activeTurboChargingSlots.Count() + 1); // +1 because we want to add one turbo charger.
                    turboChargingSlot.UpdatePowerDrain(newTurboPower);
                }

                return;
            }

            // if not turbo charging requested => we need to free up 50 kW of power out of turbo charging slots
            foreach (var turboChargingSlot in activeTurboChargingSlots)
            {
                int powerToBeTakenFromTurboCharger = DefaultPowerPerSlot / activeTurboChargingSlots.Count();
                int newTurboPower = turboChargingSlot.State.Power - powerToBeTakenFromTurboCharger;
                turboChargingSlot.UpdatePowerDrain(newTurboPower);
            }
        }

        private int GetAvailablePower()
        {
            // Let's assume that only active slots are charging. But non charging slots have power drain 0 anyway.
            int currentPowerDrain = _slots.Sum(slot => slot.State.Power);
            return MaxTotalStationChargingPower - currentPowerDrain;
        }
    }
}
