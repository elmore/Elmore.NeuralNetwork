using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmore.NeuralNetwork
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
