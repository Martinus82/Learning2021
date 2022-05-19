using OmegaGroup.MathOperations;
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
            // Arrange
            MathOperation mathOperation = new();
            ConsolePrinter printer = new();
            SumOperationConsumer sumConsumer = new(mathOperation, printer);

            // Act
            sumConsumer.ConsumeSum();

            // Assert
            // How to assert if not mock objects?
        }
    }
}
