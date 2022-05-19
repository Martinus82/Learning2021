using OmegaGroup.MathOperations.Abstractions;
using System;
using System.Threading;

namespace OmegaGroup.MathOperations
{
    public class MathOperation : IMathOperation
    {
        public double Sum(double a, double b)
        {
            // Sub optimal solution.
            Thread.Sleep(TimeSpan.FromSeconds(5));
            return a + b;
        }
    }
}
