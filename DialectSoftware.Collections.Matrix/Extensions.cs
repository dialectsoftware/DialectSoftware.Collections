using System;
using System.Collections.Generic;
using System.Text;

namespace DialectSoftware.Collections
{
    public static class Extensions
    {
        public static Int64 Product(this IEnumerable<Axis> sequences)
        {
            Int64 accumulator = 1;
            foreach (var sequence in sequences)
            {
                double d = ((double)((sequence.Max - sequence.Min) + 1)) / (double)sequence.Slope;
                accumulator *= (Int64)Math.Ceiling(d);
            }
            return accumulator;
        }
    }
}
