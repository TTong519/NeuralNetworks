using CommonLib;

namespace NNAsXOR
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NeuralNetwork nn = new(ActivationFuncs.Sigmoid, ErrorFuncs.MSE, ActivationFuncs.Identity.Compute, 2, 2, 1);
            double[,] inputs = { { 0, 0 }, { 0, 1 }, { 1, 0 }, { 1, 1 } };
            double[,] desiredOutputs = { { 0 }, { 1 }, { 1 }, { 0 } };
            for (int i = 0; i < 1000; i++)
            {
                nn.Train(inputs, desiredOutputs, 0.1);
            }
            foreach (var input in inputs)
            {
                double[] output = nn.Compute(input);
                Console.WriteLine($"Input: {string.Join(", ", input)} => Output: {string.Join(", ", output)}");
            }
        }
    }
}
