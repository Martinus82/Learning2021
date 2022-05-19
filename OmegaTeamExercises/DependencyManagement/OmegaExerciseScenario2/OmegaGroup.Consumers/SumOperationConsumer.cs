using System;
using System.Globalization;
using OmegaGroup.MathOperations;

namespace OmegaGroup.Consumers
{
    public class SumOperationConsumer
    {
        private readonly MathOperation _mathOperation;
        private readonly ConsolePrinter _printer;

        public SumOperationConsumer(
            MathOperation mathOperation,
            ConsolePrinter printer)
        {
            _mathOperation = mathOperation ?? throw new ArgumentNullException(nameof(mathOperation));
            _printer = printer ?? throw new ArgumentNullException(nameof(printer));
        }

        public void ConsumeSum()
        {
            // a, b could com from the user input...
            double a = 11;
            double b = 22;
            double result = _mathOperation.Sum(a, b);
            _printer.Print(result.ToString(CultureInfo.InvariantCulture));
        }
    }
}
