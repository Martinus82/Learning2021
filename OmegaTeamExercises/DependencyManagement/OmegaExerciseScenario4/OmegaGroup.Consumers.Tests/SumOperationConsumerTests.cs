using System;

using FluentAssertions;

using Moq;

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
            bool mathOperationCalled = false;
            bool printerCalled = false;

            // Arrange
            MathOperationConsumer sumConsumer = new(
               (x, y) =>
                {
                    mathOperationCalled = true;
                    return 0;
                },
            s =>
            {
                printerCalled = true;
            });

            // Act
            sumConsumer.ConsumeSum();

            // Assert
            mathOperationCalled.Should().BeTrue();
            printerCalled.Should().BeTrue();
        }
    }
}
