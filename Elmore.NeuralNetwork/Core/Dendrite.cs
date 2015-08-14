
namespace Elmore.NeuralNetwork.Core
{
    public class Dendrite : ISingleInput, ISingleOutput, ITrainable
    {
        private ISingleOutput Input { get; set; }
        private readonly double _learningRate;
        public double Weight { get; set; }

        public Dendrite(double weight = 0, double learningRate = 0.1)
        {
            Weight = weight;
            _learningRate = learningRate;
        }

        public double Output()
        {
            var signal = Input.Output();

            return (signal * Weight);
        }

        public void SetConnection(ISingleOutput input)
        {
            Input = input;
        }

        public void Update(double error)
        {
            double correction = _learningRate * error;

            Weight += correction * Input.Output();
        }
    }
}
