using System;
using System.Collections.Generic;
using System.Linq;

namespace Elmore.NeuralNetwork
{
    public class Perceptron
    {
        private readonly Neuron _neuron = new Neuron(0);
        private readonly List<Dendrite> _dendrites = new List<Dendrite>();
        private readonly List<Input> _inputs = new List<Input>();
        private const double _learningRate = 0.1;

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

        public double Train(double desiredOutput, int[] pattern)
        {
            Console.WriteLine("Pattern :\t{0},\t{1},\t{2}  ->  {3}", pattern[0], pattern[1], pattern[2], desiredOutput);

            double output = Classify(pattern);

            Console.WriteLine("Output :\t{0}", output);

            double err = desiredOutput - output;

            Console.WriteLine("Error :\t\t{0}", err);

            double correction = _learningRate * err;

            Console.WriteLine("Correction :\t{0}", correction);

            foreach (Dendrite d in _dendrites)
            {
                d.Weight += correction * d.Input;
            }

            _neuron.Threshold = _neuron.Threshold - (_learningRate * err);


            string weights = string.Empty;
            for (int i = 0; i < _dendrites.Count; i++)
            {
                weights += string.Format("{0},\t", _dendrites[i].Weight);
            }
            Console.WriteLine("Weights :\t{0}\r", weights);

            return Math.Abs(err);
        }

        public double Train(List<KeyValuePair<double, int[]>> dataset, double maxAllowedError = 0.0, int maxIterations = 100)
        {
            double totalErr = double.MaxValue;

            int i = 0;
            while (totalErr > maxAllowedError && i < maxIterations)
            {
                totalErr += dataset.Sum(pair => Train(pair.Key, pair.Value));
                i++;
            }

            return totalErr;
        }
    }
}
