namespace Elmore.NeuralNetwork.Core
{
    public class Input : ISingleOutput
    {
        public double Signal { get; set; }

        public Input(double signal = 0)
        {
            Signal = signal;
        }

        public double Output()
        {
            return Signal;
        }
    }
}
