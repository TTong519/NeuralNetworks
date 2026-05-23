using CommonLib;
namespace PerceptronTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            double[] testWeights = { 0.75, -1.25 };
            double[,] testInputs =
            {
                {0, 0},
                {0.3, -0.7},
                {1, 1},
                {-1, -1},
                {-0.5, 0.5}
            };
            Perceptron perceptron = new(testWeights, 0.5);
            double[] testOut = perceptron.Compute(testInputs);
            ;
        }
    }
}
