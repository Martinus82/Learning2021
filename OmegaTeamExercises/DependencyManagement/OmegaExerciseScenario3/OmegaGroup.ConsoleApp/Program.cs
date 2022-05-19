using System;
using OmegaGroup.ConsolePrinters;
using OmegaGroup.Consumers;
using OmegaGroup.MathOperations;

namespace OmegaGroup.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsolePrinter printer = new ConsolePrinter();
            MathOperation mathOperation = new MathOperation();

            Func<double, double, double> sumOperation = (x, y) => mathOperation.Sum(x, y);
            Action<string> printAction = message => printer.Print(message);

            SumOperationConsumer consumer = new SumOperationConsumer(sumOperation, printAction);
            consumer.ConsumeSum();
        }
    }
}
