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
        public decimal[] Select { get; set; }

        public IntegerUnit(CompareMethod compareMethod = CompareMethod.Any)
        {
            CompareMethod = compareMethod;
        }   

        public IntegerUnit(CharsToIntegerPart ctip)
        {
            CompareMethod = ctip.CompareMethod;
            Value1 = ctip.Value1;
            Value2 = ctip.Value2;
        }

        public IntegerUnit(decimal exactValue, CompareMethod compareMethod = CompareMethod.Exact)
        {
            CompareMethod = compareMethod;
            Value1 = exactValue;
        }

        public IntegerUnit(decimal minValue, decimal maxValue, CompareMethod compareMethod = CompareMethod.MinMax)
        {
            CompareMethod = compareMethod;
            Value1 = minValue;
            Value2 = maxValue;
        }

        public override bool Compare(object b)
        {
            if (!decimal.TryParse(b.ToString(), out decimal d))
                return false;
            else if (Math.Round(d) != d) // Not Integer
                return false;

            switch (CompareMethod)
            {
                case CompareMethod.Any:
                    return true;
                case CompareMethod.Exact:
                    return Value1 == d;
                case CompareMethod.Not:
                    return Value1 != d;
                case CompareMethod.MinMax:
                    return d >= Value1 && d <= Value2;
                case CompareMethod.NotMinMax:
                    return d < Value1 || d > Value2;
                case CompareMethod.Select:
                    if (Select == null)
                        return false;
                    for (int i = 0; i < Select.Length; i++)
                        if (Select[i] == d)
                            return true;
                    return false;
                case CompareMethod.NotSelect:
                    if (Select == null)
                        return true;
                    for (int i = 0; i < Select.Length; i++)
                        if (Select[i] == d)
                            return false;
                    return true;
                case CompareMethod.Special:
                default:
                    throw new ArgumentOutOfRangeException(nameof(CompareMethod));
            }
        }

        public override object Random()
        {
            if (CompareMethod == CompareMethod.Exact)
                return Value1;
            Random rnd = new Random((int)DateTime.Now.Ticks);
            decimal d;

            switch (CompareMethod)
            {   
                case CompareMethod.Any:
                    return new decimal(rnd.Next(), rnd.Next(), rnd.Next(), rnd.Next(2) == 1, 0);
                case CompareMethod.Not:
                    do { d = new decimal(rnd.Next(), rnd.Next(), rnd.Next(), rnd.Next(2) == 1, 0); }
                    while (d == Value1);
                    return d;
                case CompareMethod.MinMax:
                    if (Value1 > Value2)
                        throw new ArgumentException(nameof(Value1) + nameof(Value2));
                    return Math.Round((decimal)rnd.NextDouble() * (Value2 - Value1) + Value1);
                case CompareMethod.NotMinMax:
                    //Scan
                    decimal ri = (decimal)rnd.NextDouble() * (Value1 - decimal.MinValue + decimal.MaxValue - Value2);
                    if (ri < Value1)
                        return ri;
                    else
                        return (Value2 + ri - Value1 + decimal.MinValue);
                case CompareMethod.Select:
                    if (Select == null || Select.Length == 0)
                        return null;
                    return Select[rnd.Next(Select.Length)];
                case CompareMethod.NotSelect:
                    if (Select == null || Select.Length == 0)
                        goto case CompareMethod.Any;
                    while (true)
                    {
                        d = new decimal(rnd.Next(), rnd.Next(), rnd.Next(), rnd.Next(2) == 1, 0);
                        for (int i = 0; i < Select.Length; i++)
                            if (Select[i] == d)
                                continue;
                        return d;
                    }
                case CompareMethod.Special:                  
                default:
                    throw new ArgumentOutOfRangeException(nameof(CompareMethod));
            }
        }
    }
}



