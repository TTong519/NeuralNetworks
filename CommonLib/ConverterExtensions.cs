using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLib
{
    public static class ConverterExtensions
    {
        public static List<double> ToDoubleList(this string str)
        {
            var parts = str.ToCharArray();
            var result = new List<double>();
            foreach(var part in parts)
            {
                result.Add(part);
            }
            return result;
        }
    }
}
