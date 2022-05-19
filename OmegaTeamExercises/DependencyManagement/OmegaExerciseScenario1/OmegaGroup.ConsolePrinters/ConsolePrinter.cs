using System;
using OmegaGroup.Printers.Abstractions;

namespace OmegaGroup.ConsolePrinters
{
    public class ConsolePrinter : IPrinter
    {
        public void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
