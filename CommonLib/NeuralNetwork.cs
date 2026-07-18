using System;
using System.Collections.Generic;

namespace CommonLib
{
    public class NeuralNetwork
    {
        public Layer[] Layers { get; private set; }
        public ErrorFunc Error { get; private set; }
        public Func<double, double> Filter { get; private set; }
        public NeuralNetwork(ActivationFunc activation, ErrorFunc errorFunc, Func<double, double> filter, params int[] layerSizes)
        {
            Layers = new Layer[layerSizes.Length];
            Error = errorFunc;
            Filter = filter;
            Layers[0] = new InputLayer(layerSizes[0], activation);
            for (int i = 1; i < Layers.Length; i++)
            {
                Layers[i] = new Layer(layerSizes[i], activation, Layers[i - 1]);
            }
        }
        public void Randomize(Random random, double min, double max)
        {
            foreach (Layer l in Layers)
            {
                l.Randomize(random, min, max);
            }
        }
        public double[] Compute(double[] inputs)
        {
            ((InputLayer)Layers[0]).Inputs = inputs;
            for (int i = 0; i < Layers.Length; i++)
            {
                Layers[i].Compute();
            }
            List<double> outputs = new List<double>();
            foreach (double output in Layers[Layers.Length - 1].Outputs)
            {
                outputs.Add(Filter(output));
            }
            return outputs.ToArray();
        }
        public void Mutate(double mutationRate, double mutationAmount)
        {
            int numWeights = 0;
            foreach (Layer l in Layers)
            {
                foreach (Neuron n in l.Neurons)
                {
                    numWeights += n.Dendrites.Length + 1;
                }
            }
            int numMutations = (int)(mutationRate * numWeights);
            for(int i = numMutations; i > 0; i--)
            {
                int layerIndex = Random.Shared.Next(1, Layers.Length);
                Layer layer = Layers[layerIndex];
                int neuronIndex = Random.Shared.Next(layer.Neurons.Length);
                Neuron neuron = layer.Neurons[neuronIndex];
                if (Random.Shared.NextDouble() < 0.5)
                {
                    neuron.Bias += (Random.Shared.NextDouble() * 2 - 1) * mutationAmount;
                }
                else
                {
                    int dendriteIndex = Random.Shared.Next(neuron.Dendrites.Length);
                    neuron.Dendrites[dendriteIndex].Weight += (Random.Shared.NextDouble() * 2 - 1) * mutationAmount;
                }
            }
        }
        public void ApplyMomentum(double momentum)
        {
            foreach (Layer l in Layers)
            {
                foreach (Neuron n in l.Neurons)
                {
                    n.BiasUpdate += n.LastBiasUpdate * momentum;
                    foreach (Dendrite d in n.Dendrites)
                    {
                        d.WeightUpdate += d.LastWeightUpdate * momentum;
                    }
                }
            }
        }
        public void ApplyUpdate()
        {
            foreach(Layer l in Layers)
            {
                l.ApplyUpdate();
            }
        }
        public void Backprop(double learningRate, double[] desiredOutputs, bool resetDeltas = true)
        {
            if (resetDeltas)
            {
                for (int layer = 1; layer < Layers.Length; layer++)
                {
                    foreach (Neuron n in Layers[layer].Neurons)
                    {
                        n.Delta = 0;
                    }
                }
            }

            for(int i = 0; i < Layers.Last().Neurons.Length; i++)
            {
                double errorDerivative = Error.ComputeDerivative(Layers.Last().Neurons[i].Output, desiredOutputs[i]);
                if (resetDeltas)
                {
                    Layers.Last().Neurons[i].Delta = errorDerivative;
                }
                else
                {
                    Layers.Last().Neurons[i].Delta += errorDerivative;
                }
            }
            for(int i = Layers.Length - 1; i > 0; i--)
            {
                Layers[i].Backprop(learningRate);
            }
        }
        public double[] Train(double[][] inputs, double[][] desiredOutputs, double learningRate, double momentum)
        {
            List<double> errors = new List<double>();
            for (int i = 0; i < inputs.Length; i++)
            {
                List<double> error = new List<double>();
                double[] outs = Compute(inputs[i]);
                for (int j = 0; j < outs.Length; j++)
                {
                    error.Add(Error.Compute(outs[j], desiredOutputs[i][j]));
                }
                errors.Add(error.Average());
                Backprop(learningRate, desiredOutputs[i]);
                ApplyMomentum(momentum);
                ApplyUpdate();
            }
            return errors.ToArray();
        }
        public double[] BatchTrain(double[][] inputs, double[][] desiredOutputs, double learningRate, double momentum, double batchSize)
        {
            List<double> batchErrors = new List<double>();
            int numSamples = inputs.Length;
            int actualBatchSize = (int)batchSize;
            for (int batchStart = 0; batchStart < numSamples; batchStart += actualBatchSize)
            {
                int batchEnd = batchStart + actualBatchSize > numSamples ? numSamples : batchStart + actualBatchSize;
                List<double> currentBatchErrors = new List<double>();
                for (int i = batchStart; i < batchEnd; i++)
                {
                    double[] outs = Compute(inputs[i]);
                    for (int j = 0; j < outs.Length; j++)
                    {
                        currentBatchErrors.Add(Error.Compute(outs[j], desiredOutputs[i][j]));
                    }
                    bool isFirstSampleInBatch = (i == batchStart);
                    Backprop(learningRate, desiredOutputs[i], resetDeltas: isFirstSampleInBatch);
                }
                ApplyMomentum(momentum);
                ApplyUpdate();
                batchErrors.Add(currentBatchErrors.Average());
            }
            return batchErrors.ToArray();
        }
    }
}
