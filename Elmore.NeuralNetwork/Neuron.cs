using System.Collections.Generic;
using System.Linq;

namespace Elmore.NeuralNetwork
{
    public class Neuron : IMulitInput, ISingleOutput
    {
        private readonly List<ISingleOutput> _dendrites = new List<ISingleOutput>();

        public double Threshold { get; set; }

        public Neuron(double threshold = 0.5)
        {
            Threshold = threshold;
        }

        public void Connect(ISingleOutput dendrite)
        {
            _dendrites.Add(dendrite);
        }

        public double Output()
        {
            return ActivationFunc();
        }

        private double ActivationFunc()
        {
            double summation = _dendrites.Sum(d => d.Output());

            return (summation > Threshold) ? 1 : 0;
        }
    }
}
