using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public enum CompareMethod
    {
        Exact = 0,
        MinMax = 1
    }

    public class IntegerUnit : IUnit
    {
        public CompareMethod CompareMethod { get; set; }
        public decimal Value1 { get; set; } //min exact
        public decimal Value2 { get; set; } //max

        public IntegerUnit()
            : this(0)
        { }

        public IntegerUnit(decimal exactValue)
        {
            CompareMethod = CompareMethod.Exact;
            Value1 = exactValue;
        }

        public IntegerUnit(decimal minValue, decimal maxValue)
        {
            CompareMethod = CompareMethod.MinMax;
            Value1 = minValue;
            Value2 = maxValue;
        }

        public bool Compare(object b)
        {
            if (!decimal.TryParse(b.ToString(), out decimal d))
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
                return Math.Round((decimal)rnd.NextDouble() * (Value2 - Value1) + Value1);// Scan
            }
                
        }
    }
}



