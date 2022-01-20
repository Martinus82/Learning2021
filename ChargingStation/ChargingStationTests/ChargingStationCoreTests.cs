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
    }
}
