using System.Collections.Generic;
using System.Linq;

namespace Elmore.NeuralNetwork.Core
{
    public class StepNeuron : INeuron
    {
        private readonly List<ISingleOutput> _dendrites = new List<ISingleOutput>();

        private double _threshold;

        public StepNeuron(double threshold = 0.5)
        {
            _threshold = threshold;
        }

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
            _threshold -= correction;
        }

        private double ActivationFunc()
        {
            double summation = _dendrites.Sum(d => d.Output());

            return (summation > _threshold) ? 1 : 0;
        }
    }
}
