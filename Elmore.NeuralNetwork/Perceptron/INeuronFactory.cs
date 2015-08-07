using Elmore.NeuralNetwork.Core;

namespace Elmore.NeuralNetwork.Perceptron
{
    public interface INeuronFactory
    {
        INeuron Create();
    }
}