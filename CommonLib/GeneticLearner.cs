using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    public class GeneticLearner
    {
        public int populationSize { get; private set; }
        public int swapCount { get; private set; }
        public int[] layerSizes { get; private set; }
        public ErrorFunc Error { get; private set; }
        public ActivationFunc Activation { get; private set; }
        public Func<NeuralNetwork, double> fitnessFunc { get; private set; }
        public List<NeuralNetwork> population { get; private set; }
        public GeneticLearner(int populationSize, int swapCount, Func<NeuralNetwork, double> fitnessFunc, ErrorFunc errorFunc, ActivationFunc activationFunc, params int[] layerSizes)
        {
            this.populationSize = populationSize;
            this.swapCount = swapCount;
            this.fitnessFunc = fitnessFunc;
            this.Error = errorFunc;
            this.Activation = activationFunc;
            this.layerSizes = layerSizes;
            population = new List<NeuralNetwork>();

            for (int i = 0; i < populationSize; i++)
            {
                population.Add(new NeuralNetwork(activationFunc, errorFunc, Math.Identity, layerSizes));
            }
        }
        private void crossover(NeuralNetwork recipient, NeuralNetwork donor)
        {
            for(int i = 0; i < swapCount; i++)
            {
                int layerIndex = Random.Shared.Next(1, recipient.Layers.Length);
                int neuronIndex = Random.Shared.Next(recipient.Layers[layerIndex].Neurons.Length);
                int dendriteIndex = Random.Shared.Next(recipient.Layers[layerIndex].Neurons[neuronIndex].Dendrites.Length);
                recipient.Layers[layerIndex].Neurons[neuronIndex].Dendrites[dendriteIndex].Weight = donor.Layers[layerIndex].Neurons[neuronIndex].Dendrites[dendriteIndex].Weight;
                recipient.Layers[layerIndex].Neurons[neuronIndex].Bias = donor.Layers[layerIndex].Neurons[neuronIndex].Bias;
            }
        }
        public void Learn(int generations, double mutationRate, double mutationAmount)
        {
            for (int i = 0; i < generations; i++)
            {
                Dictionary<double, List<NeuralNetwork>> dict = new Dictionary<double, List<NeuralNetwork>>();
                List<double> fitnesses = new List<double>();
                foreach (NeuralNetwork n in population)
                {
                    double fitness = fitnessFunc(n);
                    if (!dict.ContainsKey(fitness))
                    {
                        dict[fitness] = new List<NeuralNetwork>();
                    }
                    dict[fitness].Add(n);
                    fitnesses.Add(fitness);
                }
                fitnesses.Sort();
                List<NeuralNetwork> bestNets = new List<NeuralNetwork>();
                for (int j = populationSize - 1; j > populationSize - (populationSize * 0.1) - 1; j--)
                {
                    foreach (NeuralNetwork net in dict[fitnesses[j]])
                    {
                        bestNets.Add(net);
                        j--;
                        if (j <= populationSize - (populationSize * 0.1) - 1)
                        {
                            break;
                        }
                    }
                }
                List<NeuralNetwork> survivorNets = new List<NeuralNetwork>();
                for (int j = populationSize - (int)(populationSize * 0.1) - 1; j > populationSize - (populationSize * 0.5) - 1; j--)
                {
                    foreach (NeuralNetwork net in dict[fitnesses[j]])
                    {
                        survivorNets.Add(net);
                        j--;
                        if (j <= populationSize - (populationSize * 0.5) - 1)
                        {
                            break;
                        }
                    }
                }
                foreach (NeuralNetwork n in survivorNets)
                {
                    foreach (NeuralNetwork best in bestNets)
                    {
                        crossover(n, best);
                    }
                    n.Mutate(mutationRate, mutationAmount);
                }
                population.Clear();
                population.AddRange(bestNets);
                population.AddRange(survivorNets);
                while(population.Count < populationSize)
                {
                    NeuralNetwork toAdd = new NeuralNetwork(Activation, Error, Math.Identity, layerSizes);
                    toAdd.Mutate(mutationRate, mutationAmount);
                    population.Add(toAdd);
                }
            }
        }
    }
}
