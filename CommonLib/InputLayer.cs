using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    public class InputLayer : Layer
    {
        public double[] Inputs { get; set; }
        public InputLayer(int numNeurons, ActivationFunc activation) : base(numNeurons, activation, null)
        {
            Inputs = new double[numNeurons];
            for(int i = 0; i < numNeurons; i++)
            {
                Neurons[i] = new(activation, Array.Empty<Neuron>());
            }
        }
        public override double[] Compute()
        {
            for (int i = 0; i < Neurons.Length; i++)
            {
                Neurons[i].Output = Inputs[i];
                Outputs[i] = Neurons[i].Output;
            }
            return Outputs;
        }
    }
}
