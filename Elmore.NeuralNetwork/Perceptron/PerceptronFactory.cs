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
    }
}