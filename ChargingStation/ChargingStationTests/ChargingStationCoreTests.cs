using System;

using ChargingStationCore;

using FluentAssertions;
using Xunit;

namespace ChargingStationTests
{
    public class ChargingStationCoreTests
    {
        [Fact]
        public void CreateInstance()
        {
            ChargingStation chargingStation = new ChargingStation();

            chargingStation.Should().NotBeNull();
        }

        [Fact]
        public void Initial_Charging_State_Should_Be_NonCharging()
        {
            ChargingStation chargingStation = new ChargingStation();

            chargingStation.State.Should().Be(ChargingState.NonCharging);
        }

        [Fact]
        public void Charging_State_Should_Be_Charging_When_Charging_Started() //_Plug_Connected() ??? What's wrong with this name ???
        {
            ChargingStation chargingStation = new ChargingStation();

            chargingStation.StartCharging();

            chargingStation.State.Should().Be(ChargingState.Charging);
        }

        [Fact]
        public void Charging_State_Should_Be_NonCharging_When_Charging_Ended()
        {
            ChargingStation chargingStation = CreateStationInChargingState();

            chargingStation.EndCharging();

            chargingStation.State.Should().Be(ChargingState.NonCharging);
        }

        [Fact]
        public void Multiple_Slots_Charging()
        {
            ChargingStation chargingStation = new ChargingStation();

            chargingStation.StartCharging();

            chargingStation.State.Should().Be(ChargingState.NonCharging);
        }

        private ChargingStation CreateStationInChargingState()
        {
            ChargingStation chargingStation = new();
            chargingStation.StartCharging();
            return chargingStation;
        }
    }
}