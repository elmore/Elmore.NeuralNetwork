using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;
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

            var imgLocation = @"..\..\img";

            var helper = new InputProcessor(imgLocation);
            byte[] aData = helper.JpgToBinaryArr("a.jpg");
            byte[] bData = helper.JpgToBinaryArr("b.jpg");

            var trainingSet = new Dictionary<string, byte[]>
            {
                { "A", aData },
                { "B", bData }
            };

            network.Train(trainingSet);

            Assert.AreEqual("A", network.Classify(aData));
            Assert.AreEqual("B", network.Classify(bData));
        }
    }

    public class InputProcessor
    {
        private readonly string _basePath;

        public InputProcessor(string basePath)
        {
            _basePath = basePath;
        }

        public string FullPath(string filename)
        {
            return Path.Combine(_basePath, filename);
        }

        public byte[] JpgToBinaryArr(string file)
        {
            string path = FullPath(file);

            Image img = Image.FromFile(path);

            using (var ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Jpeg);

                return ms.ToArray();
            }
        }
    }
}
