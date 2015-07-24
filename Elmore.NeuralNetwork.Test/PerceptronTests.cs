using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Elmore.NeuralNetwork.Test
{
    [TestFixture]
    public class PerceptronTests
    {
        [Test]
        public void CanConnectNeurons()
        {
            var in1 = new Input();
            var in2 = new Input();
            var in3 = new Input();

            var dendrite1 = new Dendrite();
            dendrite1.SetConnection(in1);

            var dendrite2 = new Dendrite();
            dendrite2.SetConnection(in2);

            var dendrite3 = new Dendrite();
            dendrite3.SetConnection(in3);

            var neuron = new Neuron();

            neuron.Connect(dendrite1);
            neuron.Connect(dendrite2);
            neuron.Connect(dendrite3);

            var dendrite4 = new Dendrite();

            dendrite4.SetConnection(neuron);

            Assert.AreEqual(0d, neuron.Output());
        }

        [Test]
        public void DendriteMultipliesInputByWeight()
        {
            var input = new Input(1.1);
            var dendrite = new Dendrite(10);

            dendrite.SetConnection(input);

            Assert.AreEqual(11, dendrite.Output());
        }

        [TestCase(0.1, 2, 0.5, 0)]
        [TestCase(0.2, 2, 0.5, 1)]
        [TestCase(0.13, 2, 0.5, 1)]
        [TestCase(0.12, 2, 0.5, 0)]
        public void NeuronFiresRelativeToThreshold(double val, double weight, double threshold, double output)
        {
            var input1 = new Input(val);
            var dendrite1 = new Dendrite(weight);
            dendrite1.SetConnection(input1);

            var input2 = new Input(val);
            var dendrite2 = new Dendrite(weight);
            dendrite2.SetConnection(input2);

            var neuron = new Neuron(threshold);
            neuron.Connect(dendrite1);
            neuron.Connect(dendrite2);

            Assert.AreEqual(output, neuron.Output());
        }

        [Test]
        public void CanReadBitmapsIntoArrs()
        {
            var aSet = new InputProcessor(@"..\..\trainingsets\dataset1\A");

            int[] arr = aSet.BmpToBinaryArr(@"1.bmp");

            Assert.AreEqual(100, arr.Length);
        }


        [Test]
        public void ClassifiesNAND()
        {
            var network = new PerceptronFactory().BuildPerceptron(3);

            var trainingSet = new List<KeyValuePair<double, int[]>>
            {
                new KeyValuePair<double, int[]>( 1.0, new [] {1, 0, 0 } ),
                new KeyValuePair<double, int[]>( 1.0, new [] {1, 0, 1 } ),
                new KeyValuePair<double, int[]>( 1.0, new [] {1, 1, 0 } ),
                new KeyValuePair<double, int[]>( 0.0, new [] {1, 1, 1 } ),
            };

            network.Train(trainingSet);

            Assert.AreEqual(1.0, network.Classify(new[] { 1, 0, 0 }));
            Assert.AreEqual(0.0, network.Classify(new[] { 1, 1, 1 }));
        }

        [Test]
        public void ClassifiesSmallImages()
        {
            var network = new PerceptronFactory().BuildPerceptron(100);

            var aSet = new InputProcessor(@"..\..\trainingsets\dataset1\A");
            var bSet = new InputProcessor(@"..\..\trainingsets\dataset1\B");

            var trainingSet = new List<KeyValuePair<double, int[]>>
            {
                new KeyValuePair<double, int[]>( 1.0, aSet.BmpToBinaryArr(@"1.bmp") ),
                new KeyValuePair<double, int[]>( 1.0, aSet.BmpToBinaryArr(@"2.bmp") ),
                new KeyValuePair<double, int[]>( 1.0, aSet.BmpToBinaryArr(@"3.bmp") ),
                new KeyValuePair<double, int[]>( 1.0, aSet.BmpToBinaryArr(@"4.bmp") ),
                new KeyValuePair<double, int[]>( 1.0, aSet.BmpToBinaryArr(@"5.bmp") ),

                new KeyValuePair<double, int[]>( 0.0, bSet.BmpToBinaryArr(@"1.jpg") ),
                new KeyValuePair<double, int[]>( 0.0, bSet.BmpToBinaryArr(@"2.jpg") ),
                new KeyValuePair<double, int[]>( 0.0, bSet.BmpToBinaryArr(@"3.jpg") ),
                new KeyValuePair<double, int[]>( 0.0, bSet.BmpToBinaryArr(@"4.jpg") ),
                new KeyValuePair<double, int[]>( 0.0, bSet.BmpToBinaryArr(@"5.jpg") ),
            };

            network.Train(trainingSet);

            var aData = aSet.BmpToBinaryArr(@"5.jpg");
            var bData = bSet.BmpToBinaryArr(@"5.jpg");

            Assert.AreEqual(1.0, network.Classify(aData));
            Assert.AreEqual(0.0, network.Classify(bData));
        }
    }
}
