using static System.Runtime.InteropServices.MemoryMarshal;
namespace CommonLib
{
    public class Perceptron
    {
        public double[] Weights { get; private set; }
        public double Bias { get; private set; }
        public ErrorFunc Error { get; private set; }
        public double MutationAmount { get; private set; }
        public Random random { get; private set; }
        public ActivationFunc Activation { get; private set; }

        public Perceptron(double[] initWeights, double initBias, double mutationAmount, ErrorFunc error, ActivationFunc activation, Random rand)
        {
            Weights = initWeights;
            Bias = initBias;
            MutationAmount = mutationAmount;
            Error = error;
            Activation = activation;
            random = rand;
        }
        public Perceptron(int numInputs, double mutationAmount, ErrorFunc error, ActivationFunc activation, Random rand)
        {
            Weights = new double[numInputs];
            MutationAmount = mutationAmount;
            Error = error;
            Activation = activation;
            random = rand;
        }
        public void Randomise(double min, double max)
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
            return Activation.Compute(sum + Bias);
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
        public double GetError(double[,] inputs, double[] targets)
        {
            double[] result = new double[inputs.GetLength(0)];
            result = Compute(inputs);
            for(int i = 0; i < result.Length; i++)
            {
                result[i] = Error.Compute(result[i], targets[i]);
            }
            return result.Average();
        }
        public void Mutate()
        {
            int index = random.Next(0, Weights.Length + 1);
            if (index == Weights.Length)
            {
                Bias += (random.NextDouble() - 0.5) * 2 * MutationAmount;
            }
            else
            {
                Weights[index] += (random.NextDouble() - 0.5) * 2 * MutationAmount;
            }
        }
        public double TrainWithHillClimbing(double[,] inputs, double[] targets, double currentError)
        {
            double[] lastWeights = Weights.ToArray();
            double lastBias = Bias;
            Mutate();
            double newError = GetError(inputs, targets);
            if (newError < currentError)
            {
                return newError;
            }
            else
            {
                Weights = lastWeights;
                Bias = lastBias;
                return currentError;
            }
        }
    }
}
