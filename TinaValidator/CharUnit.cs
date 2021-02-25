using System;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class CharUnit : Unit
    {
        public CompareMethod CompareMethod { get; set; }
        public char Value1 { get; set; } //min exact not
        public char Value2 { get; set; } //max
        public char[] Select { get; private set; }

        public CharUnit()
            => CompareMethod = CompareMethod.Any;

        public CharUnit(char exactValue)
        {
            CompareMethod = CompareMethod.Exact;
            Value1 = exactValue;
        }
        public CharUnit(char minValue, char maxValue)
        {
            CompareMethod = CompareMethod.MinMax;
            Value1 = minValue;
            Value2 = maxValue;
        }

        public CharUnit(string selectChars)
            : this(selectChars.ToCharArray())
        { }
        public CharUnit(char[] select)
        {
            CompareMethod = CompareMethod.Select;
            Select = select;
        }

        public override bool Compare(object b)
        {
            if (!(b is char c))
                return false;
            switch (CompareMethod)
            {
                case CompareMethod.Any:
                    return true;
                case CompareMethod.Exact:
                    return Value1 == c;
                case CompareMethod.Not:
                    return Value1 != c;
                case CompareMethod.MinMax:
                    return c >= Value1 && c <= Value2;
                case CompareMethod.Select:
                    if (Select == null)
                        return false;
                    for (int i = 0; i < Select.Length; i++)
                        if (Select[i] == c)
                            return true;
                    return false;
                case CompareMethod.Special:
                default:
                    return false;
            }
        }

        public override object Random()
        {   
            Random rnd = new Random((int)DateTime.Now.Ticks);
            char c;
            switch (CompareMethod)
            {
                case CompareMethod.Exact:
                    return Value1;
                case CompareMethod.Any:
                    return (char)rnd.Next(char.MaxValue + 1);
                case CompareMethod.Not:
                    do
                    {
                        c = (char)rnd.Next(char.MaxValue + 1);
                    }
                    while (c == Value1);
                    return c;
                case CompareMethod.MinMax:
                    if (Value1 > Value2)
                        throw new ArgumentException();
                    return (char)rnd.Next(Value1, Value2 + 1);
                case CompareMethod.Select:
                    if (Select == null || Select.Length == 0)
                        return false;
                    return Select[rnd.Next(Select.Length)];
                case CompareMethod.Special:
                default:
                    return false;
            }   
        }
    }
}
