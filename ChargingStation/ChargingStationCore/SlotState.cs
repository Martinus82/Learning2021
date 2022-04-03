namespace ChargingStationCore
{
    public class SlotState
    {
        public SlotState()
        {
        }

        public ChargingState ChargingState { get; internal set; }
        public int Power { get; internal set; }
    }
}