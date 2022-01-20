using System;

namespace ChargingStationCore
{
    public class ChargingStation
    {
        public ChargingState State { get; private set; } // Why is important to have private set?

        public void StartCharging(Slot slot)
        {
            if (State == ChargingState.NonCharging)
            {
                State = ChargingState.Charging;
            }
        }

        public void StopCharging(Slot slot)
        {
            if (State == ChargingState.Charging)
            {
                State = ChargingState.NonCharging;
            }
        }
    }
}
