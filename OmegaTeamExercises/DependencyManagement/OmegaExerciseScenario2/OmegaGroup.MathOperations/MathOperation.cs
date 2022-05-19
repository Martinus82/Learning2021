using System;
using System.Threading;

namespace OmegaGroup.MathOperations
{
    public class MathOperation
    {
        public double Sum(double a, double b)
        {
            // Sub optimal solution.
            Thread.Sleep(TimeSpan.FromSeconds(5));
            return a + b;
        }

        //public double Diff(double a, double b)
        //{
        //    // Sub optimal solution.
        //    Thread.Sleep(TimeSpan.FromSeconds(5));
        //    return a - b;
        //}
    }
}
