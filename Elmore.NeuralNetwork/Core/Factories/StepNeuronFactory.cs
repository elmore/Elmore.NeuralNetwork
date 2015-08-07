namespace Elmore.NeuralNetwork.Core.Factories
{
    public class StepNeuronFactory : INeuronFactory
    {
        public INeuron Create()
        {
            return new StepNeuron(0);
        }
    }
}