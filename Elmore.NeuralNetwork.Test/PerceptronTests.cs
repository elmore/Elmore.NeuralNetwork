﻿using System;
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


            string line = string.Empty;
            for (int i=1;i<=arr.Length;i++)
            {
                line += (arr[i-1] == 1 ? "#" : ".");
                
                if (i%10 == 0)
                {
                    Console.WriteLine(line);
                    line = string.Empty;
                }
            }

            Assert.AreEqual(100, arr.Length);
        }

        [Test]
        public void ClassifiesSmallImages()
        {
            var network = new PerceptronFactory().BuildPerceptron(100);

            var aSet = new InputProcessor(@"..\..\trainingsets\dataset1\A");
            var bSet = new InputProcessor(@"..\..\trainingsets\dataset1\B");

            var trainingSet = new List<KeyValuePair<double, byte[]>>
            {
                new KeyValuePair<double, byte[]>( 1.0, aSet.JpgToBinaryArr(@"1.bmp") ),
                new KeyValuePair<double, byte[]>( 1.0, aSet.JpgToBinaryArr(@"2.bmp") ),
                new KeyValuePair<double, byte[]>( 1.0, aSet.JpgToBinaryArr(@"3.bmp") ),
                new KeyValuePair<double, byte[]>( 1.0, aSet.JpgToBinaryArr(@"4.bmp") ),
                new KeyValuePair<double, byte[]>( 1.0, aSet.JpgToBinaryArr(@"5.bmp") ),

                new KeyValuePair<double, byte[]>( 0.0, bSet.JpgToBinaryArr(@"1.jpg") ),
                new KeyValuePair<double, byte[]>( 0.0, bSet.JpgToBinaryArr(@"2.jpg") ),
                new KeyValuePair<double, byte[]>( 0.0, bSet.JpgToBinaryArr(@"3.jpg") ),
                new KeyValuePair<double, byte[]>( 0.0, bSet.JpgToBinaryArr(@"4.jpg") ),
                new KeyValuePair<double, byte[]>( 0.0, bSet.JpgToBinaryArr(@"5.jpg") ),
            };

            network.Train(trainingSet);

            var aData = aSet.JpgToBinaryArr(@"6.jpg");
            var bData = bSet.JpgToBinaryArr(@"6.jpg");

            Assert.AreEqual(network.Classify(aData), 1.0);
            Assert.AreEqual(network.Classify(bData), 0.0);
        }
    }
}
