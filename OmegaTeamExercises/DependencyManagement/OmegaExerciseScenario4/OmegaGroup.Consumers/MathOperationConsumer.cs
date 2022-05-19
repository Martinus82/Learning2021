using System;
using System.Globalization;

namespace OmegaGroup.Consumers
{
    public class MathOperationConsumer
    {
        private Func<double, double, double> _mathOperation;
        private Action<string> _printAction;

        public MathOperationConsumer(
            Func<double, double, double> mathOperation,
            Action<string> printer)
        {
            _mathOperation = mathOperation ?? throw new ArgumentNullException(nameof(mathOperation));
            _printAction = printer ?? throw new ArgumentNullException(nameof(printer));
        }

        public void ConsumeSum()
        {
            // a, b could com from the user input...
            double a = 11;
            double b = 22;
            double result = _mathOperation(a, b);
            _printAction(result.ToString(CultureInfo.InvariantCulture));
        }
    }
}
