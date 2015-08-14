using System.Collections.Generic;
using System.Linq;

namespace Elmore.NeuralNetwork.Core
{
    public class StepNeuron : INeuron
    {
        private double _threshold;
        private readonly List<ISingleOutput> _dendrites = new List<ISingleOutput>();        
        private readonly double _learningRate;

        public StepNeuron(double threshold = 0.5, double learningRate = 0.1)
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
        }

        public void Update(double error)
        {
            double correction = _learningRate * error;

            _threshold -= correction;

            foreach (var d in _dendrites)
            {
                var trainable = d as ITrainable;

                if (trainable != null)
                {
                    trainable.Update(correction);
                }
            }
        }

        private double ActivationFunc()
        {
            double summation = _dendrites.Sum(d => d.Output());

            return (summation > _threshold) ? 1 : 0;
        }
    }
}
