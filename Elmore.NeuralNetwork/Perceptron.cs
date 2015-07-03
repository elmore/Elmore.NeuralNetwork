
using System;
using System.Collections.Generic;

namespace Elmore.NeuralNetwork
{
    public class Perceptron
    {
        private readonly Neuron _neuron = new Neuron(0);


        public string Classify(byte[] arr)
        {
            return (int)_neuron.Output() == 1 ? "" : "";
        }

        public void AddInput(ISingleOutput simpleInput)
        {
            var dendrite = new Dendrite();

            dendrite.SetConnection(simpleInput);

            _neuron.Connect(dendrite);
        }

        public void Train(string desiredOutput, byte[] pattern)
        {

        }

        public void Train(Dictionary<string, byte[]> dataset)
        {
            foreach (KeyValuePair<string, byte[]> pair in dataset)
            {
                Train(pair.Key, pair.Value);
            }
        }
    }
}
