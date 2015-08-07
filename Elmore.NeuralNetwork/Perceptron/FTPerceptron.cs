using System;
using System.Collections.Generic;
using System.Linq;
using Elmore.NeuralNetwork.Core;
using Elmore.NeuralNetwork.Core.Factories;

namespace Elmore.NeuralNetwork.Perceptron
{
    /// <summary>
    /// this perceptron implementation uses a linear activation function, 
    /// with a fixed threshold. the handling of bias is accomplished with
    /// a weighted input locked to value 1.0
    /// </summary>
    public class FTPerceptron
    {
        private readonly INeuron _neuron;
        private readonly List<Dendrite> _dendrites = new List<Dendrite>();
        private readonly List<Input> _inputs = new List<Input>();
        private const double _learningRate = 0.1;

        public FTPerceptron(INeuronFactory neuronFactory)
        {
            // get a neuron - could be anything since this encapsulates 
            // the learning rule
            _neuron = neuronFactory.Create();

            // this is the unit input which will handle any bias
            var simpleInput = new Input(1.0);

            // dendrite has weight
            var dendrite = new Dendrite();

            // keep ref for training
            _dendrites.Add(dendrite);

            // connect to the input
            dendrite.SetConnection(simpleInput);

            // connect to the neuron
            _neuron.Connect(dendrite);
        }

        public double Classify(double[] arr)
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

        public double Train(double desiredOutput, double[] pattern)
        {
            // see what it does right now
            double output = Classify(pattern);

            // get the delta as error
            double err = desiredOutput - output;

            // calculate the amount to correct by
            double correction = _learningRate * err;

            // update the weighted dendrites
            _dendrites.ForEach(d => d.Update(correction));

            // return the modulus error for halting the training loop
            return Math.Abs(err);
        }

        public double Train(List<KeyValuePair<double, double[]>> dataset, double maxAllowedError = 0.0, int maxIterations = 100)
        {
            double totalErr = double.MaxValue;

            int i = 0;
            while (totalErr > maxAllowedError && i < maxIterations)
            {
                totalErr = dataset.Sum(pair => Train(pair.Key, pair.Value));
                i++;
            }

            if (i == maxIterations)
            {
                Console.WriteLine("Hit max iterations before error reached {0}", maxAllowedError);
            }

            return totalErr;
        }
    }
}
