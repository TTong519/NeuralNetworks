using static CommonLib.Math;
using CommonLib;
namespace PerceptronAsAnd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new();
            Perceptron perceptron = new(2, 0.01, ErrorFuncs.MAE, ActivationFuncs.Identity, random);
            perceptron.Randomise(-1, 1);
            double[][] inputs = new double[][]
            {
                new[] { 0.0, 0.0 },
                new[] { 0.0, 1.0 },
                new[] { 1.0, 0.0 },
                new[] { 1.0, 1.0 }
            };
            double[] targets = new double[] 
            { 
                0, 
                0, 
                0, 
                1 
            };
            for (int i = 0; i < 10000000; i++)
            {
                perceptron.TrainWithHillClimbing(inputs, targets, perceptron.GetError(inputs, targets));
            }
            var outs = perceptron.Compute(inputs);
            foreach (var output in outs)
            {
                Console.WriteLine(System.Math.Round(output));
            }
        }
    }
}
