using System;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class CharUnit : Unit
    {
        public CompareMethod CompareMethod { get; set; }
        public char Value1 { get; set; } //min exact not exact
        public char Value2 { get; set; } //max
        public char[] Select { get; private set; }

        public CharUnit()
            => CompareMethod = CompareMethod.Any;

        public CharUnit(char exactValue, CompareMethod compareMethod = CompareMethod.Exact)
        {
            CompareMethod = compareMethod;
            Value1 = exactValue;
        }
        public CharUnit(char minValue, char maxValue, CompareMethod compareMethod = CompareMethod.MinMax)
        {
            CompareMethod = compareMethod;
            Value1 = minValue;
            Value2 = maxValue;
        }

        public CharUnit(string selectChars, CompareMethod compareMethod = CompareMethod.Select)
            : this(selectChars.ToCharArray(), compareMethod)
        { }
        public CharUnit(char[] select, CompareMethod compareMethod = CompareMethod.Select)
        {
            CompareMethod = compareMethod;
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
                case CompareMethod.NotMinMax:
                    return c < Value1 || c > Value2;
                case CompareMethod.Select:
                    if (Select == null)
                        return false;
                    for (int i = 0; i < Select.Length; i++)
                        if (Select[i] == c)
                            return true;
                    return false;
                case CompareMethod.NotSelect:
                    if (Select == null)
                        return true;
                    for (int i = 0; i < Select.Length; i++)
                        if (Select[i] == c)
                            return false;
                    return true;
                case CompareMethod.Special:
                    switch(Value1)
                    {
                        case 'w':
                            return (c >= 'A' && c <= 'Z') ||
                                   (c >= 'a' && c <= 'z') ||
                                   (c >= '0' && c <= '9') ||
                                   c == '_';
                        case 'W':
                            return (c < 'A' || c > 'Z') &&
                                   (c < 'a' || c > 'z') &&
                                   (c < '0' || c > '9') &&
                                   c != '_';
                        case 's':
                            return c == ' ' || c == '\t' ||
                                   c == '\n' || c == '\f' ||
                                   c == '\r';
                        case 'S':
                            return c != ' ' && c != '\t' &&
                                   c != '\n' && c != '\f' &&
                                   c != '\r';
                        case 'd':
                            return c >= '0' && c <= '9';
                        case 'D':
                            return c < '0' || c > '9';
                        default:
                            throw new ArgumentOutOfRangeException(nameof(Value1));
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(CompareMethod));
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
                case CompareMethod.NotMinMax:
                    //Scan NeedCheck
                    int ri = rnd.Next(Value1 - char.MinValue + char.MaxValue - Value2 + 2);
                    if (ri < Value1 + 1)
                        return i;
                    else
                        return Value2 + ri - Value1 - char.MinValue - 1;
                case CompareMethod.Select:
                    if (Select == null || Select.Length == 0)
                        return false;
                    return Select[rnd.Next(Select.Length)];
                case CompareMethod.NotSelect:
                    if (Select == null || Select.Length == 0)
                        goto case CompareMethod.Any;                    
                    while(true)
                    {
                        bool noSelectValue = true;
                        c = (char)rnd.Next(char.MaxValue + 1);
                        for (int i = 0; i < Select.Length; i++)
                            if (Select[i] == c)
                                noSelectValue = false;
                        if (noSelectValue)
                            break;
                    }
                    return c;
                case CompareMethod.Special:
                    switch (Value1)
                    {
                        case 'w':
                            //return (c >= 'A' && c <= 'Z') ||
                            //       (c >= 'a' && c <= 'z') ||
                            //       (c >= '0' && c <= '9') ||
                            //       c == '_';
                        case 'W':
                            //return (c < 'A' || c > 'Z') &&
                            //       (c < 'a' || c > 'z') &&
                            //       (c < '0' || c > '9') &&
                            //       c != '_';
                        case 's':
                            //return c == ' ' || c == '\t' ||
                            //       c == '\n' || c == '\f' ||
                            //       c == '\r';
                        case 'S':
                            //return c != ' ' && c != '\t' &&
                            //       c != '\n' && c != '\f' &&
                            //       c != '\r';
                        case 'd':
                            //return c >= '0' && c <= '9';
                        case 'D':
                            //return c < '0' || c > '9';
                        default:
                            throw new ArgumentOutOfRangeException(nameof(Value1));
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(CompareMethod));
            }   
        }
    }
}
