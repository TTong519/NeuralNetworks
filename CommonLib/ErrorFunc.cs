using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    public class ErrorFunc
    {
        Func<double, double, double> Func { get; set; }
        Func<double, double, double> Derivative { get; set; }
        public ErrorFunc(Func<double, double, double> func, Func<double, double, double> derivative)
        {
            Func = func;
            Derivative = derivative;
        }
        public double Compute(double val, double target)
        {
            return Func(val, target);
        }
        public double ComputeDerivative(double val, double target)
        {
            return Derivative(val, target);
        }
    }

    public static class ErrorFuncs
    {
        public static ErrorFunc MAE = new((val, target) => System.Math.Abs(val - target), (val, target) => val > target ? 1 : -1);
        public static ErrorFunc MSE = new((val, target) => System.Math.Pow(val - target, 2), (val, target) => -2 * (val - target));
    }
}
