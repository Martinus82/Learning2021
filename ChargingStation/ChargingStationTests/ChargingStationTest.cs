using ChargingStationCore;
using FluentAssertions;
using System;
using Xunit;

namespace ChargingStationTests
{
    public class ChargingStationTest
    {
        [Fact]
        public void ChargingStationCreation()
        {
            ChargingStation chargingStation = new();//Arrange + Act

            bool isChargingStationCreated = chargingStation is not null;

            Assert.True(isChargingStationCreated);                  //Assert

        }

        [Fact]
        public void ChargingStationCreationWithFluentAssertion()
        {
            ChargingStation chargingStation = new();//Arrange + Act

            chargingStation.Should().NotBeNull();                   //Assert

        }

        [Fact]
        public void InitialChargingStateIsNonCharging()
        {
            ChargingStation chargingStation = new();      //Arrange + Act

            chargingStation.State.Should().Be(ChargingState.NonCharging); //Assert
        }

        [Fact]
        public void ChargingStationStateIsChargingWhenChargingStartedOnSlotOne()
        {
            ChargingStation chargingStation = new();      //Arrange

            chargingStation.StartCharging(SlotId.One);                      //Act

            chargingStation.State.Should().Be(ChargingState.Charging);    //Assert
        }

        [Fact]
        public void StationStateIsChargingWhenChargingStartedOnSlotTwo()
        {
            ChargingStation chargingStation = CreateStationInChargingState();      //Arrange

            chargingStation.StartCharging(SlotId.Two);                      //Act

            chargingStation.State.Should().Be(ChargingState.Charging);    //Assert
        }

        [Fact]
        public void StationStateIsNonChargingWhenAllSlotsChargingStopped()
        {
            ChargingStation chargingStation = CreateStationInChargingState();      //Arrange

            DisconnectAllSlots(chargingStation);

            chargingStation.State.Should().Be(ChargingState.NonCharging);    //Assert
        }

        private static void DisconnectAllSlots(ChargingStation chargingStation)
        {
            chargingStation.StopCharging(SlotId.One);
            chargingStation.StopCharging(SlotId.Two);
            chargingStation.StopCharging(SlotId.Three);
            chargingStation.StopCharging(SlotId.Four);
        }

        [Fact]
        public void AllSlotsConnected_DisconnectAllSlots_StationStateIsNonCharging()
        {
            ChargingStation chargingStation = CreateStationInChargingStateAllSlotsConnected();      //Arrange

            DisconnectAllSlots(chargingStation);

            chargingStation.State.Should().Be(ChargingState.NonCharging);    //Assert
        }

        private ChargingStation CreateStationInChargingStateAllSlotsConnected()
        {
            ChargingStation chargingStation = new();
            chargingStation.StartCharging(SlotId.One);
            chargingStation.StartCharging(SlotId.Two);
            chargingStation.StartCharging(SlotId.Three);
            chargingStation.StartCharging(SlotId.Four);
            return chargingStation;
        }

        private ChargingStation CreateStationInChargingState()
        {
            ChargingStation chargingStation = new();
            chargingStation.StartCharging(SlotId.One);
            return chargingStation;
        }

        [Fact]
        public void ChargingStationStateIsNonChargingWhenChargingEndedOnSlotOne()
        {
            ChargingStation chargingStation = new();//Arrange

            chargingStation.StopCharging(SlotId.One);          //Act

            chargingStation.State.Should().Be(ChargingState.NonCharging);    //Assert 
        }

        [Fact]
        public void StationAllSlotsNonCharging()
        {
            ChargingStation chargingStation = new();

            chargingStation.State.Should().Be(ChargingState.NonCharging);
        }

        [Fact]
        public void SlotOneStartCharging()
        {
            ChargingStation chargingStation = new();

            chargingStation.StartCharging(SlotId.One);

            SlotState slotState = chargingStation.GetSlotState(SlotId.One);

            slotState.ChargingState.Should().Be(ChargingState.Charging);
        }

        [Fact]
        public void SlotOneStartChargingSlotTwoShouldNotBeCharging()
        {
            ChargingStation chargingStation = new();

            chargingStation.StartCharging(SlotId.One);

            SlotState slotState = chargingStation.GetSlotState(SlotId.Two);

            slotState.ChargingState.Should().Be(ChargingState.NonCharging);
            //*******
            chargingStation.State.Should().Be(ChargingState.Charging);
        }

        [Fact]
        public void SlotOneStopChargingWhenSlotOneIsCharging()
        {
            ChargingStation chargingStation = new();
            chargingStation.StartCharging(SlotId.One);

            chargingStation.StopCharging(SlotId.One);

            SlotState slotState = chargingStation.GetSlotState(SlotId.One);
            slotState.ChargingState.Should().Be(ChargingState.NonCharging);
            //*******
            chargingStation.State.Should().Be(ChargingState.NonCharging);
        }

        [Fact]
        public void SlotOneStopChargingWhenSlotsOneAndTwoAreCharging()
        {
            ChargingStation chargingStation = CreateStationWithTwoSlotsCharging();

            chargingStation.StopCharging(SlotId.One);

            SlotState slotOneState = chargingStation.GetSlotState(SlotId.One);
            SlotState slotTwoState = chargingStation.GetSlotState(SlotId.Two);

            slotOneState.ChargingState.Should().Be(ChargingState.NonCharging);
            slotTwoState.ChargingState.Should().Be(ChargingState.Charging);
            //*******
            chargingStation.State.Should().Be(ChargingState.Charging);
        }

        private static ChargingStation CreateStationWithTwoSlotsCharging()
        {
            ChargingStation chargingStation = new();
            chargingStation.StartCharging(SlotId.One);
            chargingStation.StartCharging(SlotId.Two);
            return chargingStation;
        }

        [Fact]
        public void SlotOneStartChargingPowerIs50()
        {
            ChargingStation chargingStation = new();

            chargingStation.StartCharging(SlotId.One);

            SlotState slotOneState = chargingStation.GetSlotState(SlotId.One);
            slotOneState.Power.Should().Be(50);
        }

        [Fact]
        public void SlotOneStopChargingPowerIs0()
        {
            ChargingStation chargingStation = CreateStationWithSlotOneCharging();

            chargingStation.StopCharging(SlotId.One);

            SlotState slotOneState = chargingStation.GetSlotState(SlotId.One);
            slotOneState.Power.Should().Be(0);
        }

        private static ChargingStation CreateStationWithSlotOneCharging()
        {
            ChargingStation chargingStation = new();
            chargingStation.StartCharging(SlotId.One);
            return chargingStation;
        }

        [Fact]
        public void SlotOneAndTwoStartChargingPowerIs50Each()
        {
            ChargingStation chargingStation = CreateStationWithTwoSlotsCharging();

            SlotState slotOneState = chargingStation.GetSlotState(SlotId.One);
            SlotState slotTwoState = chargingStation.GetSlotState(SlotId.Two);

            slotOneState.Power.Should().Be(50);
            slotTwoState.Power.Should().Be(50);
        }

        [Fact]
        public void SlotOneStopChargingWhenSlotsOneAndTwoAreChargingPowerSlotOneIs0()
        {
            ChargingStation chargingStation = CreateStationWithTwoSlotsCharging();

            chargingStation.StopCharging(SlotId.One);

            SlotState slotOneState = chargingStation.GetSlotState(SlotId.One);

            slotOneState.Power.Should().Be(0);
        }

        [Fact]
        public void StationPowerIs50WhenOneNonTurboSlotIsCharging()
        {
            ChargingStation chargingStation = CreateStationWithSlotOneCharging();

            chargingStation.Power.Should().Be(50);
        }

        [Fact]
        public void StationPowerIs100WhenTwoNonTurboSlotsAreCharging()
        {
            ChargingStation chargingStation = CreateStationWithTwoSlotsCharging();

            chargingStation.Power.Should().Be(100);
        }

        [Fact]
        public void StationPowerIs200WhenFourNonTurboSlotsAreCharging()
        {
            ChargingStation chargingStation = CreateStationWithFourSlotsCharging();

            chargingStation.Power.Should().Be(200);
        }

        private static ChargingStation CreateStationWithFourSlotsCharging()
        {
            ChargingStation chargingStation = new();
            chargingStation.StartCharging(SlotId.One);
            chargingStation.StartCharging(SlotId.Two);
            chargingStation.StartCharging(SlotId.Three);
            chargingStation.StartCharging(SlotId.Four);
            return chargingStation;
        }

    }
}
