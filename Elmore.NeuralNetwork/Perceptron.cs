
namespace Elmore.NeuralNetwork
{
    public class Perceptron
    {
        private readonly Neuron _neuron = new Neuron(0);


        public int Classify(int[] arr)
        {
            return (int)_neuron.Output();
        }

        public void AddInput(ISingleOutput simpleInput)
        {
            var dendrite = new Dendrite();

            dendrite.SetConnection(simpleInput);

            _neuron.Connect(dendrite);
        }

        public void Train(string desiredOutput, int[] pattern)
        {

        }
    }
}
