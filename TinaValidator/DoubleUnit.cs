using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class DoubleUnit : IUnit
    {
        public CompareMethod CompareMethod { get; set; }
        public double Value1 { get; set; } //min exact
        public double Value2 { get; set; } //max

        public bool Compare(object b)
        {
            if (!double.TryParse(b.ToString(), out double d))
                return false;
            if (CompareMethod == CompareMethod.Exact)
                return Value1 == d;
            else
                return d > Value1 && d < Value2;
        }

        public object Random()
        {
            if (CompareMethod == CompareMethod.Exact)
                return Value1;
            else
            {
                Random rnd = new Random(Convert.ToInt32(DateTime.Now.Ticks));
                return rnd.NextDouble() * (Value2 - Value1) + Value1;
            }
        }
    }
}
