using Elmore.NeuralNetwork.Core;

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
    }

    public class StepNeuronFactory : INeuronFactory
    {
        public INeuron Create()
        {
            return new StepNeuron(0);
        }
    }
}