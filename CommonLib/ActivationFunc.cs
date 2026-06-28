using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    public class ActivationFunc
    {
        Func<double, double> Func { get; set; }
        Func<double, double> Derivative { get; set; }
        public ActivationFunc(Func<double, double> func, Func<double, double> derivative)
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
    public static class ActivationFuncs
    {
        public static ActivationFunc Sigmoid = new(
            input => 1 / (1 + System.Math.Exp(-input)),
            input => input * (1 - input)
        );
        public static ActivationFunc BinStep = new(
            input => input > 0 ? 1 : 0,
            input => 0
        );
        public static ActivationFunc TanH = new(
            input => System.Math.Tanh(input),
            input => 1 - (System.Math.Tanh(input) * System.Math.Tanh(input))
        );
        public static ActivationFunc ReLU = new(
            input => System.Math.Max(0, input),
            input => input > 0 ? 1 : 0
        );
        public static ActivationFunc Identity = new(
            input => input,
            input => 1
        );
        public static ActivationFunc Round = new(
            input => System.Math.Round(input),
            input => 0
        );
    }
}
