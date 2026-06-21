using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    public class Neuron
    {
        public double Delta { get; set; }
        public double Bias { get; internal set; }
        public Dendrite[] Dendrites { get; private set; }
        public double Output { get; set; }
        public double Input { get; set; }
        public double BiasUpdate {  get; set; }
        public double LastBiasUpdate { get; set; }
        public ActivationFunc Activation { get; set; }
        public Neuron(ActivationFunc activation, Neuron[] previousNerons)
        {
            Activation = activation;
            Dendrites = new Dendrite[previousNerons.Length];
            for(int i = 0; i < Dendrites.Length; i++)
            {
                Dendrites[i] = new Dendrite(previousNerons[i], this, 0);
            }
        }
        public void Randomize(Random random, double min, double max)
        {
            Bias = ((max - min) * random.NextDouble()) + min;
            foreach (Dendrite d in Dendrites)
            {
                d.Weight = ((max - min) * random.NextDouble()) + min;
            }
        }
        public double Compute()
        {
            Input = 0;
            foreach (Dendrite d in Dendrites)
            {
                Input += d.Compute();
            }
            Input += Bias;
            Output = Activation.Compute(Input);
            return Output;
        }
        public void ApplyUpdate()
        {
            foreach (var d in Dendrites)
            {
                d.ApplyUpdate();
            }
            Bias += BiasUpdate;
            BiasUpdate = 0;
            LastBiasUpdate = BiasUpdate;
        }
        public void Backprop(double learningRate)
        {
            double activationDerivative = Activation.ComputeDerivative(Output);
            foreach(var d in Dendrites)
            {
                d.Previous.Delta = Delta * activationDerivative * d.Weight;
                d.WeightUpdate -= learningRate * Delta * activationDerivative * d.Previous.Output;
            }
            BiasUpdate -= learningRate * Delta * activationDerivative;
            Delta = 0;
        }
    }
}
