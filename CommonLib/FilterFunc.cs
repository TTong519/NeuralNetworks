using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    public class FilterFunc
    {
        Func<double, double> Func { get; set; }
        Func<double, double> Derivative { get; set; }
        public FilterFunc(Func<double, double> func, Func<double, double> derivative)
        {
            Func = func;
            Derivative = derivative;
        }
        public double Compute(double input)
        {
            return Func(input);
        }
        public double ComputeDerivative(double input)
        {
            return Derivative(input);
        }
    }

    public static class FilterFuncs
    {
        public static FilterFunc Identity = new(
            input => input,
            input => 1
        );
        public static FilterFunc Round = new(
            input => System.Math.Round(input),
            input => 0
        );
        public static FilterFunc Sigmoid = new(
            input => 1 / (1 + System.Math.Exp(-input)),
            input => input * (1 - input)
        );
        public static FilterFunc TanH = new(
            input => System.Math.Tanh(input),
            input => 1 - System.Math.Pow(System.Math.Tanh(input), 2)
        );
        public static FilterFunc ReLU = new(
            input => System.Math.Max(0, input),
            input => input > 0 ? 1 : 0
        );
    }
}
