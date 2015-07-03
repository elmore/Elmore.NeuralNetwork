using System;

namespace Elmore.NeuralNetwork
{
    public class PerceptronFactory
    {
        public Perceptron BuildPerceptron(int inputs)
        {
            var network = new Perceptron();

            for (int i = 0; i < inputs; i++)
            {
                var simpleInput = new Input();

                network.AddInput(simpleInput);
            }

            return network;
        }
    }
}