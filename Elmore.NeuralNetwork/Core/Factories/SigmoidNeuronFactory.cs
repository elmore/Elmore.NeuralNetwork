namespace Elmore.NeuralNetwork.Core.Factories
{
    public class SigmoidNeuronFactory : INeuronFactory
    {
        public INeuron Create()
        {
            return new SigmoidNeuron();
        }
    }
}