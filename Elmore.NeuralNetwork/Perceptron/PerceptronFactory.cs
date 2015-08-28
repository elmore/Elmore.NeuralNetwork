using System.Collections.Generic;
using Elmore.NeuralNetwork.Core;
using Elmore.NeuralNetwork.Core.Factories;

namespace Elmore.NeuralNetwork.Perceptron
{
    public class PerceptronFactory
    {
        public Perceptron BuildPerceptron(int inputs)
        {
            var network = new Perceptron(new StepNeuronFactory());

            for (int i = 0; i < inputs; i++)
            {
                var simpleInput = new Input();

                network.AddInput(simpleInput);
            }

            return network;
        }

        public FTPerceptron BuildFTPerceptron(int inputs)
        {
            var network = new FTPerceptron(new StepNeuronFactory());

            for (int i = 0; i < inputs; i++)
            {
                var simpleInput = new Input();

                network.AddInput(simpleInput);
            }

            return network;
        }

        public FTPerceptron BuildSigmoidFTPerceptron(int inputs)
        {
            var network = new FTPerceptron(new SigmoidNeuronFactory());

            for (int i = 0; i < inputs; i++)
            {
                var simpleInput = new Input();

                network.AddInput(simpleInput);
            }

            return network;
        }

        public MultiLayerPerceptron BuildMultiLayerPerceptron()
        {
            // 4 inputs, 3 hidden neurons, 2 outputs, fully connected

            var factory = new SigmoidNeuronFactory();

            var network = new MultiLayerPerceptron(factory);

            // input values
            var i1 = new Input();
            var i2 = new Input();
            var i3 = new Input();
            var i4 = new Input();

            // hidden layer
            var hn1 = factory.Create();
            var hn2 = factory.Create();
            var hn3 = factory.Create();
            //var hiddenLayer = new Layer(new List<INeuron> { hn1, hn2, hn3 });
            
            // output neurons
            var on1 = factory.Create();
            var on2 = factory.Create();
            //var outputLayer = new Layer(new List<INeuron> {on1, on2});

            network.AddInput(i1);
            network.AddInput(i2);
            network.AddInput(i3);
            network.AddInput(i4);

            // hidden layer
            network.AddHiddenNeuron(hn1);
            network.AddHiddenNeuron(hn2);
            network.AddHiddenNeuron(hn3);

            // output layer
            network.AddOutput(on1);
            network.AddOutput(on2);


            // hidden connections
            var dh11 = new Dendrite(learningRate: 1);
            var dh12 = new Dendrite(learningRate: 1);
            var dh21 = new Dendrite(learningRate: 1);
            var dh22 = new Dendrite(learningRate: 1);
            var dh31 = new Dendrite(learningRate: 1);
            var dh32 = new Dendrite(learningRate: 1);

            dh11.SetConnection(hn1);
            dh12.SetConnection(hn1);
            dh21.SetConnection(hn2);
            dh22.SetConnection(hn2);
            dh31.SetConnection(hn3);
            dh32.SetConnection(hn3);

            on1.Connect(dh11);
            on1.Connect(dh21);
            on1.Connect(dh31);
            on2.Connect(dh12);
            on2.Connect(dh22);
            on2.Connect(dh32);


            // input dendrite connections
            var d11 = new Dendrite(learningRate: 1);
            var d12 = new Dendrite(learningRate: 1);
            var d13 = new Dendrite(learningRate: 1);
            var d21 = new Dendrite(learningRate: 1);
            var d22 = new Dendrite(learningRate: 1);
            var d23 = new Dendrite(learningRate: 1);
            var d31 = new Dendrite(learningRate: 1);
            var d32 = new Dendrite(learningRate: 1);
            var d33 = new Dendrite(learningRate: 1);

            d11.SetConnection(i1);
            d12.SetConnection(i1);
            d13.SetConnection(i1);
            d21.SetConnection(i2);
            d22.SetConnection(i2);
            d23.SetConnection(i2);
            d31.SetConnection(i3);
            d32.SetConnection(i3);
            d33.SetConnection(i3);

            hn1.Connect(d11);
            hn1.Connect(d21);
            hn1.Connect(d31);

            hn2.Connect(d12);
            hn2.Connect(d22);
            hn2.Connect(d32);

            hn3.Connect(d13);
            hn3.Connect(d23);
            hn3.Connect(d33);

            return network;
        }
    }
}