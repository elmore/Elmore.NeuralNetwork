using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            var helper = new InputProcessor("img");
            int[] aData = helper.JpgToBinaryArr("a.jpg");
            int[] bData = helper.JpgToBinaryArr("b.jpg");

            var trainingSet = new Dictionary<string, int[]>
            {
                { "A", aData },
                { "B", bData }
            };

            network.Train(trainingSet);

            Assert.AreEqual("A", network.Classify(aData));
            Assert.AreEqual("B", network.Classify(bData));
        }
    }
}
