using System;
using System.Collections.Generic;
using System.Linq;

namespace ChargingStationCore
{
    internal class PowerDistributor
    {
        private const int MaxTotalStationChargingPower = 400;

        private readonly ICollection<Slot> _slots;

        public PowerDistributor(ICollection<Slot> slots)
        {
            _slots = slots ?? throw new ArgumentNullException(nameof(slots));
        }

        public int RequestPower(bool requestTurboCharging)
        {
            int availablePower = GetAvailablePower();
            int requestedPower = requestTurboCharging ? Slot.TurboPower : Slot.DefaultPower;
            if (availablePower >= requestedPower)
            {
                return requestedPower;
            }

            //DecreasePowerDrainOfActiveTurboSlots(requestTurboCharging);

            //int newAvailablePower = GetAvailablePower();
            //return newAvailablePower;
            return 0;
        }

        //public void RedistributePowerToActiveTurboSlot()
        //{
        //    var activeTurboChargingSlots = _slots.Where(slot => slot.State.IsTurboChargingSupported == true && slot.State.ChargingState == ChargingState.Charging);
        //    int currentPowerConsumptionOfTurboSlots = _slots.Where(s => s.State.ChargingState == ChargingState.Charging)
        //        .Sum(s => s.State.Power);

        //    int totalPowerAmountWhichCanBeRedistributed = MaxTotalStationChargingPower - currentPowerConsumptionOfTurboSlots;
        //    int numberOfActiveTurboChargers = activeTurboChargingSlots.Count();

        //    // Because default power cannot be increased, we need to push all the available power only to (between) turbo charging slots.
        //    foreach (var turboChargingSlot in activeTurboChargingSlots)
        //    {
        //        int powerWhichCanBeRedistributedPerSlot = totalPowerAmountWhichCanBeRedistributed / numberOfActiveTurboChargers;
        //        int newPower = powerWhichCanBeRedistributedPerSlot + turboChargingSlot.State.Power;
        //        turboChargingSlot.UpdatePowerDrain(newPower > TurboPower ? TurboPower : newPower);
        //    }
        //}

        public int ComputePowerDrainDecrease(bool turboRequested)
        {
            var activeTurboChargingSlots = _slots.Where(slot => slot.State.IsTurboChargingSupported == true && slot.State.ChargingState == ChargingState.Charging);
            var activeNonTurboChargingSlots = _slots.Where(slot => slot.State.IsTurboChargingSupported == false && slot.State.ChargingState == ChargingState.Charging);

            if (turboRequested)
            {
                //foreach (var turboChargingSlot in activeTurboChargingSlots)
                //{
                int totalAvailableTurboPower = MaxTotalStationChargingPower - activeNonTurboChargingSlots.Sum(n => n.State.Power);
                int newTurboPower = totalAvailableTurboPower / (activeTurboChargingSlots.Count() + 1); // +1 because we want to add one turbo charger.
                return newTurboPower;
                //turboChargingSlot.UpdatePowerDrain(newTurboPower);
                //yield return newTurboPower;
                //}
            }

            int totalAvailableTurboPower1 = MaxTotalStationChargingPower - Slot.DefaultPower - activeNonTurboChargingSlots.Sum(n => n.State.Power);
            int newTurboPower1 = totalAvailableTurboPower1 / activeTurboChargingSlots.Count();
            return newTurboPower1;

        }

        public int ComputePowerDrainIncrease()
        {
            var activeTurboChargingSlots = _slots.Where(slot => slot.State.IsTurboChargingSupported == true && slot.State.ChargingState == ChargingState.Charging);
            var activeNonTurboChargingSlots = _slots.Where(slot => slot.State.IsTurboChargingSupported == false && slot.State.ChargingState == ChargingState.Charging);

            // if not turbo charging requested => we need to free up 50 kW of power out of turbo charging slots
            //foreach (var turboChargingSlot in activeTurboChargingSlots)
            //{
            //int powerToBeTakenFromTurboCharger = Slot.DefaultPower / activeTurboChargingSlots.Count();
            //return powerToBeTakenFromTurboCharger;
            //int newTurboPower = turboChargingSlot.State.Power - powerToBeTakenFromTurboCharger;
            ////turboChargingSlot.UpdatePowerDrain(newTurboPower);
            //yield return newTurboPower;
            //}

            int totalAvailableTurboPower = MaxTotalStationChargingPower - activeNonTurboChargingSlots.Sum(n => n.State.Power);
            int newTurboPower = totalAvailableTurboPower / activeTurboChargingSlots.Count();
            return newTurboPower;
        }

        public int GetAvailablePower()
        {
            // Let's assume that only active slots are charging. But non charging slots have power drain 0 anyway.
            int currentPowerDrain = _slots.Sum(slot => slot.State.Power);
            return MaxTotalStationChargingPower - currentPowerDrain;
        }
    }
}
