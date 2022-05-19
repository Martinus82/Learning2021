using Moq;
using OmegaGroup.MathOperations.Abstractions;
using OmegaGroup.Printers.Abstractions;
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
            Mock<IMathOperation> mathOperationMock = new();
            Mock<IPrinter> printerMock = new();
            SumOperationConsumer sumConsumer = new(mathOperationMock.Object, printerMock.Object);

            mathOperationMock.Setup(o => o.Sum(It.IsAny<double>(), It.IsAny<double>())).Returns((double)default);
            printerMock.Setup(p => p.Print(It.IsAny<string>()));

            // Act
            sumConsumer.ConsumeSum();

            // Assert
            mathOperationMock.Verify(o => o.Sum(It.IsAny<double>(), It.IsAny<double>()), Times.Once);
            printerMock.Verify(p => p.Print(It.IsAny<string>()), Times.Once);
        }
    }
}
