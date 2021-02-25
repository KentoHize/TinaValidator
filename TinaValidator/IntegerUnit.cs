using System;

namespace Aritiafel.Artifacts.TinaValidator
{
    public enum CompareMethod
    {
        Any = 0,        
        Exact = 1,
        Not = 2,
        MinMax = 3,
        NotMinMax = 4,
        Select = 5,
        NotSelect = 6,
        Special = 7
    }

    public class IntegerUnit : Unit
    {
        public CompareMethod CompareMethod { get; set; }
        public decimal Value1 { get; set; } //min exact
        public decimal Value2 { get; set; } //max

        public IntegerUnit()
            => CompareMethod = CompareMethod.Any;

        public IntegerUnit(CharsToIntegerPart ctip)
        {
            CompareMethod = ctip.CompareMethod;
            Value1 = ctip.Value1;
            Value2 = ctip.Value2;
        }

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

        public override bool Compare(object b)
        {
            if (!decimal.TryParse(b.ToString(), out decimal d))
                return false;
            else if (Math.Round(d) != d) // Not Integer
                return false;
            if (CompareMethod == CompareMethod.Any)
                return true;
            else if (CompareMethod == CompareMethod.Exact)
                return Value1 == d;
            else
                return d >= Value1 && d <= Value2;
        }

        public override object Random()
        {
            if (CompareMethod == CompareMethod.Exact)
                return Value1;
            Random rnd = new Random((int)DateTime.Now.Ticks);
            if (CompareMethod == CompareMethod.MinMax)
            {
                if (Value1 > Value2)
                    throw new ArgumentException();
                return Math.Round((decimal)rnd.NextDouble() * (Value2 - Value1) + Value1);
            }
            else
                return new decimal(rnd.Next(), rnd.Next(), rnd.Next(), rnd.Next(2) == 1, 0);
        }
    }
}



