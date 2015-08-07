using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Elmore.NeuralNetwork.Core;
using Elmore.NeuralNetwork.Perceptron;
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

            var neuron = new StepNeuron();

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

            var neuron = new StepNeuron(threshold);
            neuron.Connect(dendrite1);
            neuron.Connect(dendrite2);

            Assert.AreEqual(output, neuron.Output());
        }

        [Test]
        public void CanReadBitmapsIntoArrs()
        {
            var aSet = new InputProcessor(@"..\..\trainingsets\dataset1\A");

            double[] arr = aSet.BmpToBinaryArr(@"1.jpg");

            Assert.AreEqual(100, arr.Length);
        }

        [Test]
        public void CanReadTabSeparatedList()
        {
            var reader = new TabSeparatedListReader();

            List<KeyValuePair<double, double[]>> set = reader.Read(@"..\..\trainingsets\dataset2\biased.txt");

            Assert.AreEqual(208, set.Count);
        }

        [Test]
        public void ClassifiesNAND()
        {
            var network = new PerceptronFactory().BuildPerceptron(3);

            var trainingSet = new List<KeyValuePair<double, double[]>>
            {
                new KeyValuePair<double, double[]>( 1.0, new [] {1.0, 0.0, 0.0 } ),
                new KeyValuePair<double, double[]>( 1.0, new [] {1.0, 0.0, 1.0 } ),
                new KeyValuePair<double, double[]>( 1.0, new [] {1.0, 1.0, 0.0 } ),
                new KeyValuePair<double, double[]>( 0.0, new [] {1.0, 1.0, 1.0 } ),
            };

            network.Train(trainingSet);

            Assert.AreEqual(1.0, network.Classify(new [] { 1.0, 0.0, 0.0 }));
            Assert.AreEqual(0.0, network.Classify(new [] { 1.0, 1.0, 1.0 }));
        }

        [Test]
        public void ClassifiesSmallImages()
        {
            var network = new PerceptronFactory().BuildPerceptron(100);

            var aSet = new InputProcessor(@"..\..\trainingsets\dataset1\A");
            var bSet = new InputProcessor(@"..\..\trainingsets\dataset1\B");

            var trainingSet = new List<KeyValuePair<double, double[]>>
            {
                new KeyValuePair<double, double[]>( 1.0, aSet.BmpToBinaryArr(@"1.jpg") ),
                new KeyValuePair<double, double[]>( 1.0, aSet.BmpToBinaryArr(@"2.jpg") ),
                new KeyValuePair<double, double[]>( 1.0, aSet.BmpToBinaryArr(@"3.jpg") ),
                new KeyValuePair<double, double[]>( 1.0, aSet.BmpToBinaryArr(@"4.jpg") ),
                new KeyValuePair<double, double[]>( 1.0, aSet.BmpToBinaryArr(@"5.jpg") ),

                new KeyValuePair<double, double[]>( 0.0, bSet.BmpToBinaryArr(@"1.jpg") ),
                new KeyValuePair<double, double[]>( 0.0, bSet.BmpToBinaryArr(@"2.jpg") ),
                new KeyValuePair<double, double[]>( 0.0, bSet.BmpToBinaryArr(@"3.jpg") ),
                new KeyValuePair<double, double[]>( 0.0, bSet.BmpToBinaryArr(@"4.jpg") ),
                new KeyValuePair<double, double[]>( 0.0, bSet.BmpToBinaryArr(@"5.jpg") ),
            };

            network.Train(trainingSet);

            trainingSet.ForEach(kvp => Assert.AreEqual(kvp.Key, network.Classify(kvp.Value)));
        }

        [Test]
        public void GeneralisesSmallImages()
        {
            var network = new PerceptronFactory().BuildPerceptron(100);

            var aSet = new InputProcessor(@"..\..\trainingsets\dataset1\A");
            var bSet = new InputProcessor(@"..\..\trainingsets\dataset1\B");

            var trainingSet = new List<KeyValuePair<double, double[]>>
            {
                new KeyValuePair<double, double[]>( 1.0, aSet.BmpToBinaryArr(@"1.jpg") ),
                new KeyValuePair<double, double[]>( 1.0, aSet.BmpToBinaryArr(@"2.jpg") ),
                new KeyValuePair<double, double[]>( 1.0, aSet.BmpToBinaryArr(@"3.jpg") ),
                new KeyValuePair<double, double[]>( 1.0, aSet.BmpToBinaryArr(@"4.jpg") ),
                new KeyValuePair<double, double[]>( 1.0, aSet.BmpToBinaryArr(@"5.jpg") ),

                new KeyValuePair<double, double[]>( 0.0, bSet.BmpToBinaryArr(@"1.jpg") ),
                new KeyValuePair<double, double[]>( 0.0, bSet.BmpToBinaryArr(@"2.jpg") ),
                new KeyValuePair<double, double[]>( 0.0, bSet.BmpToBinaryArr(@"3.jpg") ),
                new KeyValuePair<double, double[]>( 0.0, bSet.BmpToBinaryArr(@"4.jpg") ),
                new KeyValuePair<double, double[]>( 0.0, bSet.BmpToBinaryArr(@"5.jpg") ),
            };

            network.Train(trainingSet);

            Assert.AreEqual(1.0, network.Classify(aSet.BmpToBinaryArr(@"7.jpg")));
            Assert.AreEqual(0.0, network.Classify(bSet.BmpToBinaryArr(@"7.jpg")));
        }

        [Test]
        public void ClassifiesBiasedData()
        {
            // data has 2 double inputs and an expected binary out
            var network = new PerceptronFactory().BuildPerceptron(2);

            var reader = new TabSeparatedListReader();

            List<KeyValuePair<double, double[]>> trainingSet = reader.Read(@"..\..\trainingsets\dataset2\biased.txt");

            network.Train(trainingSet);

            trainingSet.ForEach(kvp => Assert.AreEqual(kvp.Key, network.Classify(kvp.Value)));
        }

        [Test]
        public void FTClassifiesBiasedData()
        {
            // data has 2 double inputs and an expected binary out
            var network = new PerceptronFactory().BuildFTPerceptron(2);

            var reader = new TabSeparatedListReader();

            List<KeyValuePair<double, double[]>> trainingSet = reader.Read(@"..\..\trainingsets\dataset2\biased.txt");

            network.Train(trainingSet);

            trainingSet.ForEach(kvp => Assert.AreEqual(kvp.Key, network.Classify(kvp.Value)));
        }

        [Test]
        public void SigmoidFTClassifiesNAND()
        {
            var network = new PerceptronFactory().BuildSigmoidFTPerceptron(3);

            var trainingSet = new List<KeyValuePair<double, double[]>>
            {
                new KeyValuePair<double, double[]>( 1.0, new [] {1.0, 0.0, 0.0 } ),
                new KeyValuePair<double, double[]>( 1.0, new [] {1.0, 0.0, 1.0 } ),
                new KeyValuePair<double, double[]>( 1.0, new [] {1.0, 1.0, 0.0 } ),
                new KeyValuePair<double, double[]>( 0.0, new [] {1.0, 1.0, 1.0 } ),
            };

            network.Train(trainingSet);

            Assert.AreEqual(1.0, network.Classify(new[] { 1.0, 0.0, 0.0 }));
            Assert.AreEqual(0.0, network.Classify(new[] { 1.0, 1.0, 1.0 }));
        }
    }
}
