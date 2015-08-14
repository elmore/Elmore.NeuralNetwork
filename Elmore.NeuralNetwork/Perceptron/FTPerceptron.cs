using Elmore.NeuralNetwork.Core;
using Elmore.NeuralNetwork.Core.Factories;

namespace Elmore.NeuralNetwork.Perceptron
{
    /// <summary>
    /// this perceptron implementation uses a linear activation function, 
    /// with a fixed threshold. the handling of bias is accomplished with
    /// a weighted input locked to value 1.0
    /// </summary>
    public class FTPerceptron : Perceptron
    {
        public FTPerceptron(INeuronFactory neuronFactory) : base(neuronFactory)
        {
            // this is the unit input which will handle any bias
            var simpleInput = new Input(1.0);

            // dendrite has weight
            var dendrite = new Dendrite();

            // connect to the input
            dendrite.SetConnection(simpleInput);

            // connect to the neuron
            _neuron.Connect(dendrite);
        }
    }
}
