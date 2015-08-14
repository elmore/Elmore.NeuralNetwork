using System.Collections.Generic;
using Elmore.NeuralNetwork.Core;
using Elmore.NeuralNetwork.Core.Factories;

namespace Elmore.NeuralNetwork.Perceptron
{
    // https://www4.rgu.ac.uk/files/chapter3%20-%20bp.pdf
    public class MultiLayerPerceptron
    {
        private readonly INeuronFactory _factory;

        private readonly List<Input> _inputs;
        private readonly List<INeuron> _outputs;
        private readonly List<INeuron> _hiddenLayer;

        public MultiLayerPerceptron(INeuronFactory factory)
        {
            _factory = factory;

            _inputs = new List<Input>();
            _outputs = new List<INeuron>();
            _hiddenLayer = new List<INeuron>();
        }

        public void Train(double[] target, double[] input)
        {
            double[] output = Classify(input);

            //  (1 - output[0]) term is apparently due to sigmoid function 
            // (havent grokked that yet) so maybe the error calculation 
            // should be the responsibility of the neuron?
            double err0 = (1 - output[0]) * (target[0] - output[0]);
            double err1 = (1 - output[1]) * (target[1] - output[1]);

            //_outputs[0].Update();
        }

        public void AddInput(Input input)
        {
            _inputs.Add(input);
        }

        public void AddOutput(INeuron neuron)
        {
            _outputs.Add(neuron);
        }

        public void AddHiddenNeuron(INeuron neuron)
        {
            _hiddenLayer.Add(neuron);
        }

        public double[] Classify(double[] arr)
        {
            // setup all inputs
            for (var i = 0; i < arr.Length; i++)
            {
                _inputs[i].Signal = arr[i];
            }

            // run the network
            return new [] { _outputs[0].Output(), _outputs[1].Output() };
        }

        public void Train(List<KeyValuePair<double[], double[]>> trainingSet)
        {
            throw new System.NotImplementedException();
        }
    }

    public class Layer
    {
        private readonly List<INeuron> _neurons;

        public Layer(List<INeuron> neurons)
        {
            _neurons = neurons;
        }
    }
}
