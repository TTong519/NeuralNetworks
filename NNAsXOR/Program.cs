using CommonLib;

namespace NNAsXOR
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NeuralNetwork nn = new NeuralNetwork(ActivationFuncs.Sigmoid, ErrorFuncs.MSE, CommonLib.Math.Identity, 2, 5, 3, 1);
            double[][] inputs = { new[] { 0.0, 0.0 }, new[] { 0.0, 1.0 }, new[] { 1.0, 0.0 }, new[] { 1.0, 1.0 } };
            double[][] desiredOutputs = { new[] { 0.0 }, new[] { 1.0 }, new[] { 1.0 }, new[] { 0.0 } };
            nn.Randomize(Random.Shared, -0.5, 0.5);
            double[] outputs = new double[4];
            for (int i = 0; i < 4; i++)
            {
                double[] outs = nn.Compute(inputs[i]);
                outputs[i] = outs[0];
            }
            Console.WriteLine("Init Outputs:");
            foreach (double output in outputs)
            {
                Console.WriteLine(output);
            }
            for (int i = 0; i < 100000; i++)
            {
                nn.Train(inputs, desiredOutputs, 0.5, 0.9);
                double error = 0;
                for (int j = 0; j < 4; j++)
                {
                    double[] outs = nn.Compute(inputs[j]);
                    error += ErrorFuncs.MSE.Compute(outs[0], desiredOutputs[j][0]);
                }
                if (i % 1000 == 0)
                {
                    Console.WriteLine($"Error: {error}");
                }
            }
            for (int i = 0; i < 4; i++)
            {
                double[] outs = nn.Compute(inputs[i]);
                outputs[i] = outs[0];
            }
            Console.WriteLine("Outputs:");
            foreach (double output in outputs)
            {
                Console.WriteLine(output);
            }
        }
    }
}
