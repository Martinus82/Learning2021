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
            SumOperationConsumer consumer = new SumOperationConsumer(mathOperation, printer);
            consumer.ConsumeSum();
        }
    }
}
