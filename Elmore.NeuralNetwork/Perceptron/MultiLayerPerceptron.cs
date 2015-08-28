using System;
using System.Collections.Generic;
using System.Linq;
using Elmore.NeuralNetwork.Core;
using Elmore.NeuralNetwork.Core.Factories;

namespace Elmore.NeuralNetwork.Perceptron
{
    // https://www4.rgu.ac.uk/files/chapter3%20-%20bp.pdf
    public class MultiLayerPerceptron
    {
        private readonly SigmoidNeuronFactory _factory;

        private readonly List<Input> _inputs;

        private readonly List<INeuron> _outputs;
        private readonly List<INeuron> _hiddenLayer;

        public MultiLayerPerceptron(SigmoidNeuronFactory factory)
        {
            _factory = factory;

            _inputs = new List<Input>();
            _outputs = new List<INeuron>();
            _hiddenLayer = new List<INeuron>();
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


        public double Train(double[] target, double[] input)
        {
            double learningRate = 0.1;

            double[] output = Classify(input);

            //  (1 - output[0]) term is apparently due to sigmoid function 
            // (havent grokked that yet) so maybe the error calculation 
            // should be the responsibility of the neuron?
            double errO0 = 0.5 * Math.Pow(target[0] - output[0], 2);
            double errO1 = 0.5 * Math.Pow(target[1] - output[1], 2);


            double deltah0o0 = -(target[0] - output[0]) * output[0] * (1 - output[0]) * _hiddenLayer[0].Output();
            ((Dendrite)((SigmoidNeuron)_outputs[0]).Dendrites[1]).Weight -= learningRate * deltah0o0;

            double deltah1o0 = -(target[0] - output[0]) * output[0] * (1 - output[0]) * _hiddenLayer[1].Output();
            ((Dendrite)((SigmoidNeuron)_outputs[0]).Dendrites[2]).Weight -= learningRate * deltah1o0;

            double deltah2o0 = -(target[0] - output[0]) * output[0] * (1 - output[0]) * _hiddenLayer[2].Output();
            ((Dendrite)((SigmoidNeuron)_outputs[0]).Dendrites[3]).Weight -= learningRate * deltah2o0;


            double deltah0o1 = -(target[1] - output[1]) * output[1] * (1 - output[1]) * _hiddenLayer[0].Output();
            ((Dendrite)((SigmoidNeuron)_outputs[0]).Dendrites[1]).Weight -= learningRate * deltah0o1;

            double deltah1o1 = -(target[1] - output[1]) * output[1] * (1 - output[1]) * _hiddenLayer[1].Output();
            ((Dendrite)((SigmoidNeuron)_outputs[1]).Dendrites[2]).Weight -= learningRate * deltah1o1;

            double deltah2o1 = -(target[1] - output[1]) * output[1] * (1 - output[1]) * _hiddenLayer[2].Output();
            ((Dendrite)((SigmoidNeuron)_outputs[1]).Dendrites[3]).Weight -= learningRate * deltah2o1;












            // calc 'back propagation' values
            double errWh0o0 = errO0 * ((Dendrite)((SigmoidNeuron)_outputs[0]).Dendrites[0]).Weight; // are we sure about the indexing?
            double errWh0o1 = errO1 * ((Dendrite)((SigmoidNeuron)_outputs[1]).Dendrites[0]).Weight;

            double errh0 = _hiddenLayer[0].Output() * (1 - _hiddenLayer[0].Output()) * (errWh0o0 + errWh0o1);

            double errWh1o0 = errO0 * ((Dendrite)((SigmoidNeuron)_outputs[0]).Dendrites[1]).Weight; // are we sure about the indexing?
            double errWh1o1 = errO1 * ((Dendrite)((SigmoidNeuron)_outputs[1]).Dendrites[1]).Weight;

            double errh1 = _hiddenLayer[1].Output() * (1 - _hiddenLayer[1].Output()) * (errWh1o0 + errWh1o1);

            double errWh2o0 = errO0 * ((Dendrite)((SigmoidNeuron)_outputs[0]).Dendrites[2]).Weight; // are we sure about the indexing?
            double errWh2o1 = errO1 * ((Dendrite)((SigmoidNeuron)_outputs[1]).Dendrites[2]).Weight;

            double errh2 = _hiddenLayer[2].Output() * (1 - _hiddenLayer[2].Output()) * (errWh2o0 + errWh2o1);



            // update the output layer
            _outputs[0].Update(errO0);
            _outputs[1].Update(errO1);

            // update the hidden layer
            _hiddenLayer[0].Update(errh0);
            _hiddenLayer[1].Update(errh1);
            _hiddenLayer[2].Update(errh2);


            // total error of network
            return Math.Abs(errO0) + Math.Abs(errO1);
        }


        //public double Train(double[] target, double[] input)
        //{
        //    double[] output = Classify(input);

        //    //  (1 - output[0]) term is apparently due to sigmoid function 
        //    // (havent grokked that yet) so maybe the error calculation 
        //    // should be the responsibility of the neuron?
        //    double err0 = output[0] * (1 - output[0]) * (target[0] - output[0]);
        //    double err1 = output[1] * (1 - output[1]) * (target[1] - output[1]);

        //    // calc 'back propagation' values
        //    double errWh0o0 = err0 * ((Dendrite) ((SigmoidNeuron)_outputs[0]).Dendrites[0]).Weight; // are we sure about the indexing?
        //    double errWh0o1 = err1 * ((Dendrite) ((SigmoidNeuron)_outputs[1]).Dendrites[0]).Weight;

        //    double errh0 = _hiddenLayer[0].Output() * (1 - _hiddenLayer[0].Output()) * (errWh0o0 + errWh0o1);

        //    double errWh1o0 = err0 * ((Dendrite) ((SigmoidNeuron)_outputs[0]).Dendrites[1]).Weight; // are we sure about the indexing?
        //    double errWh1o1 = err1 * ((Dendrite) ((SigmoidNeuron)_outputs[1]).Dendrites[1]).Weight;

        //    double errh1 = _hiddenLayer[1].Output() * (1 - _hiddenLayer[1].Output()) * (errWh1o0 + errWh1o1);

        //    double errWh2o0 = err0 * ((Dendrite) ((SigmoidNeuron)_outputs[0]).Dendrites[2]).Weight; // are we sure about the indexing?
        //    double errWh2o1 = err1 * ((Dendrite)((SigmoidNeuron)_outputs[1]).Dendrites[2]).Weight;

        //    double errh2 = _hiddenLayer[2].Output() * (1 - _hiddenLayer[2].Output()) * (errWh2o0 + errWh2o1);



        //    // update the output layer
        //    _outputs[0].Update(err0);
        //    _outputs[1].Update(err1);

        //    // update the hidden layer
        //    _hiddenLayer[0].Update(errh0);
        //    _hiddenLayer[1].Update(errh1);
        //    _hiddenLayer[2].Update(errh2);


        //    // total error of network
        //    return Math.Abs(err0) + Math.Abs(err1);
        //}

        public double Train(List<KeyValuePair<double[], double[]>> dataset, double maxAllowedError = 0.0, int maxIterations = 100)
        {
            double totalErr = double.MaxValue;

            int i = 0;
            while (totalErr > maxAllowedError && i < maxIterations)
            {
                totalErr = dataset.Sum(pair => Train(pair.Key, pair.Value));

                Console.WriteLine("err : {0}", totalErr);

                i++;
            }

            if (i == maxIterations)
            {
                Console.WriteLine("Hit max iterations before error reached {0}. Actual error = {1}", maxAllowedError, totalErr);
            }

            return totalErr;
        }

        //public void Train(List<KeyValuePair<double[], double[]>> trainingSet)
        //{
        //    trainingSet.ForEach(x => Train(x.Key, x.Value));
        //}
    }

    public class Layer
    {
        private readonly List<SigmoidNeuron> _neurons;

        public Layer(List<SigmoidNeuron> neurons)
        {
            _neurons = neurons;
        }
    }
}
