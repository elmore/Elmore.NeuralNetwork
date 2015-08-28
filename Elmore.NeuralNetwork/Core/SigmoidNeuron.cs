using System;
using System.Collections.Generic;
using System.Linq;

namespace Elmore.NeuralNetwork.Core
{
    public class SigmoidNeuron : INeuron
    {
        // kill this
        public List<ISingleOutput> Dendrites 
        {
            get { return _dendrites; }
        }

        private readonly double _threshold;
        private readonly List<ISingleOutput> _dendrites = new List<ISingleOutput>();
        private readonly double _learningRate;

        public SigmoidNeuron(double threshold = 0.5, double learningRate = 0.1)
        {
            _threshold = threshold;
            _learningRate = learningRate;
        }

        public void Connect(ISingleOutput dendrite)
        {
            _dendrites.Add(dendrite);
        }

        public double Output()
        {
            return ActivationFunc();
            //return ActivationFunc() >= _threshold ? 1.0 : 0;
        }

        public void Update(double error)
        {
            foreach (var d in _dendrites)
            {
                var trainable = d as ITrainable;

                if (trainable != null)
                {
                    trainable.Update(error);
                }
            }
        }

        private double ActivationFunc()
        {
            double summation = _dendrites.Sum(d => d.Output());

            return (1 / (1 + Math.Pow(Math.E, -summation)));
        }
    }
}
