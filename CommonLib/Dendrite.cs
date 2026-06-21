using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    public class Dendrite
    {
        public Neuron Previous { get; }
        public Neuron Next { get; }
        public double Weight { get; internal set; }
        public double WeightUpdate { get; set; }
        public double LastWeightUpdate { get; set; }
        public Dendrite(Neuron previous, Neuron next, double weight)
        {
            Previous = previous;
            Next = next;
            Weight = weight;
        }
        public double Compute()
        {
            return Previous.Output * Weight;
        }
        public void ApplyUpdate()
        {
            Weight += WeightUpdate;
            LastWeightUpdate = WeightUpdate;
            WeightUpdate = 0;
        }
    }
}
