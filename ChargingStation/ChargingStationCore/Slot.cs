namespace ChargingStationCore
{
    internal class Slot
    {
        public const int TurboPower = 200;
        public const int DefaultPower = 50;

        internal Slot(SlotId slotId)
        {
            SlotState = new SlotState();
            Id = slotId;
        }

        public SlotState SlotState { get; }
        public SlotId Id { get; }

        internal void StartCharging(int power)
        {
            SlotState.ChargingState = ChargingState.Charging;
            SlotState.Power = power;
        }

        internal void StartCharging(int power, bool turboChargingRequested)
        {
            SlotState.IsTurboChargingEnabled = turboChargingRequested;
            StartCharging(power is > DefaultPower and <= TurboPower && turboChargingRequested ? power : DefaultPower);
        }

        public void StopCharging()
        {
            SlotState.ChargingState = ChargingState.NonCharging;
            SlotState.Power = 0;
            SlotState.IsTurboChargingEnabled = false;
        }

        public void UpdatePowerDrain(int newPower)
        {
            newPower = newPower > TurboPower ? TurboPower : newPower;
            SlotState.Power = newPower;
        }
    }
}
