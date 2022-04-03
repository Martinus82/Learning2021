namespace ChargingStationCore
{
    public class SlotState
    {
        public ChargingState ChargingState { get; internal set; }
        public int Power { get; internal set; }
        public bool IsTurboChargingEnabled { get; internal set; } = false;
    }
}
