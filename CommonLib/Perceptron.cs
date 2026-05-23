using static System.Runtime.InteropServices.MemoryMarshal;
namespace CommonLib
{
    public class Perceptron
    {
        public double[] Weights { get; private set; }
        public double Bias { get; private set; }
        public Func<double, double, double> ErrorFunc { get; private set; }
        public double MutationAmount { get; private set; }
        public Random random { get; private set; }

        public Perceptron(double[] initWeights, double initBias, double mutationAmount, Func<double, double, double> errorFunc)
        {
            Weights = initWeights;
            Bias = initBias;
            MutationAmount = mutationAmount;
            ErrorFunc = errorFunc;
        }
        public Perceptron(int numInputs)
        {
            Weights = new double[numInputs];
        }
        public void Randomise(Random random, double min, double max)
        {
            for (int i = 0; i < Weights.Length; i++)
            {
                Weights[i] = ((max - min) * random.NextDouble()) + min;
            }
            Bias = ((max - min) * random.NextDouble()) + min;
        }
        public double Compute(double[] inputs)
        {
            double sum = 0;
            if (inputs.Length != Weights.Length) throw new ArgumentException("bad inputs");
            for(int i = 0; i < Weights.Length; i++)
            {
                sum += Weights[i] * inputs[i];
            }
            return sum + Bias;
        }
        public double[] Compute(double[,] inputs)
        {
            double[] toReturn = new double[inputs.GetLength(0)];
            if (Weights.Length != inputs.GetLength(1)) throw new ArgumentException("bad inputs");
            for(int i = 0; i < inputs.GetLength(0); i++)
            {
                Span<double> rowSpan = CreateSpan(ref inputs[i, 0], inputs.GetLength(1));
                toReturn[i] = Compute(rowSpan.ToArray());
            }
            return toReturn;
        }
        public double GetError(double[,] inputs, double[] target)
        {

        }
    }
}
