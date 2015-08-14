using System;
using System.Collections.Generic;
using System.Linq;
using Elmore.NeuralNetwork.Core;
using Elmore.NeuralNetwork.Core.Factories;

namespace Elmore.NeuralNetwork.Perceptron
{
    /// <summary>
    /// this perceptron implementation uses a linear activation function, 
    /// with a variable threshold. this allows it to deal with bias in the data
    /// 
    ///     y                              y
    ///     ^                              ^
    ///     |  - + \\  +                   |  - \\ +   +
    ///     | -    +\\ +   +               | -   \\  + +   +
    ///     | - -    \\ +                  | - -  \\    +
    ///     | -  -  + \\  +                | -  -  \\ +   +
    ///     ---------------------> x       --------------------> x
    ///        fixed threshold                variable threshold
    /// 
    /// </summary>
    public class Perceptron
    {
        protected readonly INeuron _neuron;
        protected readonly List<Input> _inputs = new List<Input>();

        public Perceptron(INeuronFactory neuronFactory)
        {
            _neuron = neuronFactory.Create();
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

            // update the neuron - this propogates back to the dendrites etc
            _neuron.Update(err);

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
