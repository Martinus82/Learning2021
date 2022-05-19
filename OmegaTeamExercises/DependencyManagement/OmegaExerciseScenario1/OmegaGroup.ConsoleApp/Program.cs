using System;
using OmegaGroup.ConsolePrinters;
using OmegaGroup.Consumers;
using OmegaGroup.MathOperations;
using OmegaGroup.MathOperations.Abstractions;
using OmegaGroup.Printers.Abstractions;

namespace OmegaGroup.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IPrinter printer = new ConsolePrinter();
            IMathOperation mathOperation = new MathOperation();
            SumOperationConsumer consumer = new SumOperationConsumer(mathOperation, printer);
            consumer.ConsumeSum();
        }
    }
}
