using System;
using System.Collections.Generic;
using System.Linq;

namespace Elmore.NeuralNetwork.Core
{
    public class SigmoidNeuron : INeuron
    {
        private readonly List<ISingleOutput> _dendrites = new List<ISingleOutput>();

        public SigmoidNeuron() { }

        public void Connect(ISingleOutput dendrite)
        {
            _dendrites.Add(dendrite);
        }

        public double Output()
        {
            return ActivationFunc();
        }

        public void Update(double correction)
        {
            // sigmoid doesnt do any update
        }

        private double ActivationFunc()
        {
            double summation = _dendrites.Sum(d => d.Output());

            return (1 / (1 + Math.Pow(Math.E, summation)));
        }
    }
}
