using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;

namespace Elmore.NeuralNetwork
{
    public class Dendrite : ISingleInput, ISingleOutput
    {
        private ISingleOutput _input { get; set; }

        public double Weight { get; set; }

        public Dendrite(double weight = 1)
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
    }
}
