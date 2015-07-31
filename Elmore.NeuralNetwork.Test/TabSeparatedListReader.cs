using System;
using System.Collections.Generic;
using System.IO;

namespace Elmore.NeuralNetwork.Test
{
    public class TabSeparatedListReader
    {
        public List<KeyValuePair<double, double[]>> Read(string path)
        {
            var retVal = new List<KeyValuePair<double, double[]>>();

            string[] lines = File.ReadAllLines(path);

            foreach (string l in lines)
            {
                string[] segments = l.Split('\t');

                double input1 = Double.Parse(segments[0]);
                double input2 = Double.Parse(segments[1]);

                double output = Double.Parse(segments[2]);

                var kvp = new KeyValuePair<double, double[]>(output, new [] { input1, input2 });

                retVal.Add(kvp);
            }

            return retVal;
        }
    }
}