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

            //MathOperationConsumer consumer = new MathOperationConsumer((x, y) => y + x, s => { });
            MathOperationConsumer consumer = new MathOperationConsumer(mathOperation.Sum, printer.Print);

            consumer.ConsumeSum();
        }
    }
}
