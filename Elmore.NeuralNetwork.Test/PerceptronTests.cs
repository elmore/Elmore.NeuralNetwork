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
        public void ClassifiesSmallImages()
        {
            var network = new PerceptronFactory().BuildPerceptron(100);

            var aSet = new InputProcessor(@"..\..\trainingsets\dataset1\A");
            var bSet = new InputProcessor(@"..\..\trainingsets\dataset1\B");

            var trainingSet = new Dictionary<string, byte[]>
            {
                { "A", aSet.JpgToBinaryArr(@"1.jpg") },
                { "A", aSet.JpgToBinaryArr(@"2.jpg") },
                { "A", aSet.JpgToBinaryArr(@"3.jpg") },
                { "A", aSet.JpgToBinaryArr(@"4.jpg") },
                { "A", aSet.JpgToBinaryArr(@"5.jpg") },

                { "B", bSet.JpgToBinaryArr(@"1.jpg") },
                { "B", bSet.JpgToBinaryArr(@"2.jpg") },
                { "B", bSet.JpgToBinaryArr(@"3.jpg") },
                { "B", bSet.JpgToBinaryArr(@"4.jpg") },
                { "B", bSet.JpgToBinaryArr(@"5.jpg") },
            };

            network.Train(trainingSet);

            var aData = aSet.JpgToBinaryArr(@"6.jpg");
            var bData = bSet.JpgToBinaryArr(@"6.jpg");

            Assert.AreEqual("A", network.Classify(aData));
            Assert.AreEqual("B", network.Classify(bData));
        }
    }
}
