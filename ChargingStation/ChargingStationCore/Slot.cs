namespace ChargingStationCore
{
    internal class Slot
    {
        internal Slot(SlotId slotId)
        {
            State = new();
            Id = slotId;
        }

        public SlotState State { get; }
        public SlotId Id { get; }
        public bool IsTurboChargingSupported { get; private set; } = false;

        internal void StartCharging(int power)
        {
            State.ChargingState = ChargingState.Charging;
            State.Power = power;
        }

        internal void StartCharging(int power, bool isTurboChargingSupported)
        {
            StartCharging(power);
            IsTurboChargingSupported = isTurboChargingSupported;
        }

        public void StopCharging()
        {
            State.ChargingState = ChargingState.NonCharging;
            State.Power = 0;
        }

        public void UpdatePowerDrain(int newPower)
        {
            State.Power = newPower;
        }
    }
}
