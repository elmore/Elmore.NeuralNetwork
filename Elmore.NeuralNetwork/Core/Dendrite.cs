
namespace Elmore.NeuralNetwork.Core
{
    public class Dendrite : ISingleInput, ISingleOutput
    {
        private ISingleOutput _input { get; set; }

        public double Weight { get; set; }

        public Dendrite(double weight = 0)
        {
            Weight = weight;
        }

        public double Output()
        {
            var signal = _input.Output();

            return (signal * Weight);
        }

        public void SetConnection(ISingleOutput input)
        {
            _input = input;
        }

        public void Update(double correction)
        {
            Weight += correction * _input.Output();
        }
    }
}
