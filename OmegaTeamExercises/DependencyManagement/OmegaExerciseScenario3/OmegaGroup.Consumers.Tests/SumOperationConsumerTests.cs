using System;
using FluentAssertions;
using Xunit;

namespace OmegaGroup.Consumers.Tests
{
    /// <summary>
    /// London - Behavioral Focused: "I don't care what is the implementation of the Sum or Print, I just care WHEN and IF they were called".
    /// Order of the calling methods could also be interesting.
    /// </summary>
    public class SumOperationConsumerTests
    {
        [Fact]
        public void TestSumConsumer()
        {
            bool sumWasCalled = false;

            // Arrange
            Func<double, double, double> mathOperationSumAction = (x, y) =>
            {
                sumWasCalled = true;
                return 0;
            };

            bool printerWasCalled = false;
            Action<string> printerAction = (message) => printerWasCalled = true;

            SumOperationConsumer sumConsumer = new(mathOperationSumAction, printerAction);

            // Act
            sumConsumer.ConsumeSum();

            // Assert
            sumWasCalled.Should().BeTrue();
            printerWasCalled.Should().BeTrue();
        }
    }
}
