using System.Collections.Generic;

namespace Elmore.NeuralNetwork
{
    public class Perceptron
    {
        private readonly Neuron _neuron = new Neuron(0);
        private readonly List<Dendrite> _dendrites = new List<Dendrite>();
        private readonly List<Input> _inputs = new List<Input>();
        private const double _learningRate = 0.2;

        public double Classify(int[] arr)
        {
            // setup all inputs
            for (var i=0; i< arr.Length; i++)
            {
                _inputs[i].Signal = arr[i];
            }

            // run the network
            return _neuron.Output();
        }

        public void AddInput(Input simpleInput)
        {
            // dendrite has weight
            var dendrite = new Dendrite();

            // keep ref for training
            _dendrites.Add(dendrite);

            // keep ref for classifying
            _inputs.Add(simpleInput);

            // connect to the input
            dendrite.SetConnection(simpleInput);

            // connect to the neuron
            _neuron.Connect(dendrite);
        }

        public void Train(double desiredOutput, int[] pattern)
        {
            double output = Classify(pattern);

            if (desiredOutput != output)
            {
                double err = desiredOutput - output;

                for (int i = 0; i < pattern.Length; i++)
                {
                    var newWeight = _dendrites[i].Weight + (_learningRate * err * pattern[i]);

                    _dendrites[i].Weight = newWeight;
                }

                double newThreshold = _neuron.Threshold - (_learningRate * err);

                _neuron.Threshold = newThreshold;
            }
        }

        public void Train(List<KeyValuePair<double, int[]>> dataset)
        {
            foreach (KeyValuePair<double, int[]> pair in dataset)
            {
                Train(pair.Key, pair.Value);
            }
        }
    }
}
