using System;
using System.Collections.Generic;
using System.Linq;

namespace Elmore.NeuralNetwork.Core
{
    public class SigmoidNeuron : INeuron
    {
        private readonly List<ISingleOutput> _dendrites = new List<ISingleOutput>();
        private readonly double _threshold = 0.9;

        public SigmoidNeuron(double threshold = 0.9)
        {
            _threshold = threshold;
        }

        public void Connect(ISingleOutput dendrite)
        {
            _dendrites.Add(dendrite);
        }

        public double Output()
        {
            return ActivationFunc() >= _threshold ? 1.0 : 0;
        }

        public void Update(double correction)
        {
            // sigmoid doesnt do any update
        }

        private double ActivationFunc()
        {
            double summation = _dendrites.Sum(d => d.Output());

            return (1 / (1 + Math.Pow(Math.E, -summation)));
        }
    }
}
