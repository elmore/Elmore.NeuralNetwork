using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;

namespace Elmore.NeuralNetwork
{
    public class Dendrite : ISingleInput, ISingleOutput
    {
        // get rid of this
        public double Input
        {
            get { return _input.Output(); }
        }

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
    }
}
