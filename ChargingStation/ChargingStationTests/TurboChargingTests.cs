using System.Collections.Generic;

using ChargingStationCore;

using FluentAssertions;

using Xunit;

namespace ChargingStationTests
{
    public class TurboChargingTests
    {
        [Fact]
        public void OneTurboChargingSlotBecomeActive()
        {
            var station = new ChargingStation();

            station.StartCharging(SlotId.One, true);

            station.ChargingState.Should().Be(ChargingState.Charging);
            station.GetSlotState(SlotId.One).ChargingState.Should().Be(ChargingState.Charging);
            station.Power.Should().Be(200);
        }

        [Fact]
        public void ThreeTurboChargingSlotBecomeActive()
        {
            var station = new ChargingStation();

            // Act
            station.StartCharging(SlotId.One, true);
            station.StartCharging(SlotId.Two, true);
            station.StartCharging(SlotId.Three, true);

            // Assert
            station.ChargingState.Should().Be(ChargingState.Charging);
            station.GetSlotState(SlotId.One).ChargingState.Should().Be(ChargingState.Charging);
            station.Power.Should().BeInRange(399, 400);

            var slotStates = new List<SlotState>
            {
                station.GetSlotState(SlotId.One),
                station.GetSlotState(SlotId.Two),
                station.GetSlotState(SlotId.Three)
            };

            slotStates.ForEach(slotState => slotState.Power.Should().BeInRange(133, 134));
        }

        [Fact]
        public void ThreeTurboSlotsAndOneDefaultBecomeActive()
        {
            var station = new ChargingStation();

            // Act
            station.StartCharging(SlotId.One, true);
            station.StartCharging(SlotId.Two, true);
            station.StartCharging(SlotId.Three, true);
            station.StartCharging(SlotId.Four);

            // Assert
            station.ChargingState.Should().Be(ChargingState.Charging);
            station.GetSlotState(SlotId.One).ChargingState.Should().Be(ChargingState.Charging);
            station.Power.Should().BeInRange(398, 400);

            var slotStates = new List<SlotState>
            {
                station.GetSlotState(SlotId.One),
                station.GetSlotState(SlotId.Two),
                station.GetSlotState(SlotId.Three)
            };

            slotStates.ForEach(slotState => slotState.Power.Should().BeInRange(116, 118));

            station.GetSlotState(SlotId.Four).Power.Should().BeInRange(48, 50);
        }

        [Fact]
        public void AllTurboSlotsBecomeActive()
        {
            var station = new ChargingStation();

            // Act
            station.StartCharging(SlotId.One, true);
            station.StartCharging(SlotId.Two, true);
            station.StartCharging(SlotId.Three, true);
            station.StartCharging(SlotId.Four, true);

            // Assert
            station.ChargingState.Should().Be(ChargingState.Charging);
            station.GetSlotState(SlotId.One).ChargingState.Should().Be(ChargingState.Charging);
            station.Power.Should().Be(400);

            var slotStates = new List<SlotState>
            {
                station.GetSlotState(SlotId.One),
                station.GetSlotState(SlotId.Two),
                station.GetSlotState(SlotId.Three),
                station.GetSlotState(SlotId.Four)
            };

            foreach (var slotState in slotStates)
            {
                slotState.ChargingState.Should().Be(ChargingState.Charging);
                slotState.Power.Should().Be(100);
            }
        }

        [Fact]
        public void ThreeDefaultSlotsAndOneTurboSlotBecomeActive()
        {
            var station = new ChargingStation();

            // Act
            station.StartCharging(SlotId.One);
            station.StartCharging(SlotId.Two);
            station.StartCharging(SlotId.Three);
            station.StartCharging(SlotId.Four, true);

            // Asset
            station.ChargingState.Should().Be(ChargingState.Charging);
            station.Power.Should().Be(350);

            var slotStates = new List<SlotState>
            {
                station.GetSlotState(SlotId.One),
                station.GetSlotState(SlotId.Two),
                station.GetSlotState(SlotId.Three)
            };

            foreach (var slotState in slotStates)
            {
                slotState.ChargingState.Should().Be(ChargingState.Charging);
                slotState.Power.Should().Be(50);
            }

            station.GetSlotState(SlotId.Four).Power.Should().Be(200);
        }

        [Fact]
        public void AllTurboChargingSlotsAreActive_OneSlotDisconnected_NewReleasedPowerCanBeRedistributedBetweenActiveTurboChargingSlots()
        {
            var station = new ChargingStation();
            station.StartCharging(SlotId.One, true);
            station.StartCharging(SlotId.Two, true);
            station.StartCharging(SlotId.Three, true);
            station.StartCharging(SlotId.Four, true);

            // Just verify :-)
            station.ChargingState.Should().Be(ChargingState.Charging);
            station.Power.Should().Be(400);

            // Act
            station.StopCharging(SlotId.Four);

            var slotStates = new List<SlotState>
            {
                station.GetSlotState(SlotId.One),
                station.GetSlotState(SlotId.Two),
                station.GetSlotState(SlotId.Three)
            };

            foreach (var slotState in slotStates)
            {
                slotState.ChargingState.Should().Be(ChargingState.Charging);
                slotState.Power.Should().Be(133);
            }

            station.GetSlotState(SlotId.Four).Power.Should().Be(0);
            station.GetSlotState(SlotId.Four).ChargingState.Should().Be(ChargingState.NonCharging);
        }
    }
}
