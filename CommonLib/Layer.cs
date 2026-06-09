using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    public class Layer
    {
        public Neuron[] Neurons { get; private set; }
        public double[] Outputs { get; private set; }
        public Layer(int numNeurons, ActivationFunc activation, Layer previousLayer)
        {
            Neurons = new Neuron[numNeurons];
            Outputs = new double[numNeurons];
            if (previousLayer == null) return;
            for (int i = 0; i < numNeurons; i++)
            {
                Neurons[i] = new(activation, previousLayer.Neurons);
            }
        }
        public void Randomize(Random random, double min, double max)
        {
            foreach (Neuron n in Neurons)
            {
                n.Randomize(random, min, max);
            }
        }
        public virtual double[] Compute()
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                Outputs[i] = Neurons[i].Compute();
            }
            return Outputs;
        }
    }
}
