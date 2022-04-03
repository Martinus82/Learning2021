namespace ChargingStationCore
{
    internal class Slot
    {
        public const int TurboPower = 200;
        public const int DefaultPower = 50;

        internal Slot(SlotId slotId)
        {
            State = new();
            Id = slotId;
        }

        public SlotState State { get; }
        public SlotId Id { get; }

        internal void StartCharging(int power)
        {
            State.ChargingState = ChargingState.Charging;
            State.Power = power;
        }

        internal void StartCharging(int power, bool isTurboChargingSupported)
        {
            State.IsTurboChargingSupported = isTurboChargingSupported;
            StartCharging(power is > DefaultPower and <= TurboPower && isTurboChargingSupported ? power : DefaultPower);
        }

        public void StopCharging()
        {
            State.ChargingState = ChargingState.NonCharging;
            State.Power = 0;
            State.IsTurboChargingSupported = false;
        }

        public void UpdatePowerDrain(int newPower)
        {
            newPower = newPower > TurboPower ? TurboPower : newPower;
            State.Power = newPower;
        }
    }
}
