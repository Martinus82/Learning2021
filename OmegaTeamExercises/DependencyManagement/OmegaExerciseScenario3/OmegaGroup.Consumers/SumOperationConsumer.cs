using System;
using System.Globalization;

namespace OmegaGroup.Consumers
{
    public class SumOperationConsumer
    {
        private readonly Func<double, double, double> _mathOperationSum;
        private readonly Action<string> _printer;

        public SumOperationConsumer(
            Func<double, double, double> mathOperationSumAction,
            Action<string> printerAction)
        {
            _mathOperationSum = mathOperationSumAction ?? throw new ArgumentNullException(nameof(mathOperationSumAction));
            _printer = printerAction ?? throw new ArgumentNullException(nameof(printerAction));
        }

        public void ConsumeSum()
        {
            // a, b could com from the user input...
            double a = 11;
            double b = 22;
            double result = _mathOperationSum.Invoke(a, b);
            _printer(result.ToString(CultureInfo.InvariantCulture));
        }
    }
}
