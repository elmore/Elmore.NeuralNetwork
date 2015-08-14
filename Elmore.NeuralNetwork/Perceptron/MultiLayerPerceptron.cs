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

        public void Train(List<KeyValuePair<double[], double[]>> trainingSet)
        {
            
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

        public double[] Classify(double[] value)
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
