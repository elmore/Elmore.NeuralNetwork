namespace Elmore.NeuralNetwork.Core
{
    public interface INeuron : IMulitInput, ISingleOutput
    {
        void Update(double correction);
    }
}