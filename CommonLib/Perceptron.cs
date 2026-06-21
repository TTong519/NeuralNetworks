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
        public double LearningRate;

        public Perceptron(double[] initWeights, double initBias, double mutationAmount, ErrorFunc error, ActivationFunc activation, Random rand, double learningRate = 1)
        {
            Weights = initWeights;
            Bias = initBias;
            MutationAmount = mutationAmount;
            Error = error;
            Activation = activation;
            random = rand;
            LearningRate = learningRate;
        }
        public Perceptron(int numInputs, double mutationAmount, ErrorFunc error, ActivationFunc activation, Random rand, double learningRate = 1)
        {
            Weights = new double[numInputs];
            MutationAmount = mutationAmount;
            Error = error;
            Activation = activation;
            random = rand;
            LearningRate = learningRate;
        }
        public void Randomise(double min, double max)
        {
            for (int i = 0; i < Weights.Length; i++)
            {
                Weights[i] = ((max - min) * random.NextDouble()) + min;
            }
            Bias = ((max - min) * random.NextDouble()) + min;
        }
        public double Compute(double[] inputs, bool doActivation = true)
        {
            double sum = 0;
            if (inputs.Length != Weights.Length) throw new ArgumentException("bad inputs");
            for(int i = 0; i < Weights.Length; i++)
            {
                sum += Weights[i] * inputs[i];
            }
            if (doActivation) return Activation.Compute(sum + Bias);
            else return sum + Bias;
        }

        public double[] Compute(double[][] inputs, bool doActivation = true)
        {
            double[] toReturn = new double[inputs.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                if (Weights.Length != inputs[i].Length) throw new ArgumentException("bad inputs");
                toReturn[i] = Compute(inputs[i], doActivation);
            }
            return toReturn;
        }
        public double GetError(double[][] inputs, double[] targets)
        {
            double[] result = new double[inputs.Length];
            result = Compute(inputs);
            for (int i = 0; i < result.Length; i++)
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
        public double TrainWithHillClimbing(double[][] inputs, double[] targets, double currentError)
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
        public double TrainWithGradientDecent(double[] inputs, double desired)
        {
            double output = Compute(inputs);
            double inactiveOutput = Compute(inputs, false);
            double pd = LearningRate * Error.ComputeDerivative(output, desired) * Activation.ComputeDerivative(inactiveOutput);
            for (int i = 0; i < Weights.Length; i++)
            {
                Weights[i] -= pd * inputs[i];
            }
            Bias -= pd;
            return Error.Compute(Compute(inputs), desired);
        }
        public double TrainWithGradientDecent(double[][] inputs, double[] desired)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                TrainWithGradientDecent(inputs[i], desired[i]);
            }
            return GetError(inputs, desired);
        }
    }
}
