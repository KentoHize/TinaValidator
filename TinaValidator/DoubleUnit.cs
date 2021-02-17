using System;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class DoubleUnit : Unit
    {
        public CompareMethod CompareMethod { get; set; }
        public double Value1 { get; set; } //min exact
        public double Value2 { get; set; } //max

        public DoubleUnit()
            => CompareMethod = CompareMethod.Any;
        public DoubleUnit(double exactValue)
        {
            CompareMethod = CompareMethod.Exact;
            Value1 = exactValue;
        }

        public DoubleUnit(double minValue, double maxValue)
        {
            CompareMethod = CompareMethod.MinMax;
            Value1 = minValue;
            Value2 = maxValue;
        }

        public override bool Compare(object b)
        {
            if (!double.TryParse(b.ToString(), out double d))
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
                return rnd.NextDouble() * (Value2 - Value1) + Value1;
            else
                return rnd.NextDouble();
        }
    }
}
