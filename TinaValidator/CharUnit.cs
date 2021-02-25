using System;
using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class CharUnit : Unit
    {   
        public static CharUnit Letter => new CharUnit { CompareMethod = CompareMethod.Special, Value1 = 'w' };
        public static CharUnit NotLetter => new CharUnit { CompareMethod = CompareMethod.Special, Value1 = 'W' };
        public static CharUnit Space => new CharUnit { CompareMethod = CompareMethod.Special, Value1 = 's' };
        public static CharUnit NotSpace => new CharUnit { CompareMethod = CompareMethod.Special, Value1 = 'S' };
        public static CharUnit Digit => new CharUnit { CompareMethod = CompareMethod.Special, Value1 = 'd' };
        public static CharUnit NotDigit => new CharUnit { CompareMethod = CompareMethod.Special, Value1 = 'D' };
        public CompareMethod CompareMethod { get; set; }
        public char Value1 { get; set; } //min exact
        public char Value2 { get; set; } //max
        public char[] Select { get; set; }

        public CharUnit(CompareMethod compareMethod = CompareMethod.Any)
        {
            CompareMethod = compareMethod;
        }
        
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
                    switch (Value1)
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
            if(CompareMethod == CompareMethod.Exact)
                return Value1;
            Random rnd = new Random((int)DateTime.Now.Ticks);
            char c;
            switch (CompareMethod)
            {       
                case CompareMethod.Any:
                    return (char)rnd.Next(char.MaxValue + 1);
                case CompareMethod.Not:
                    do { c = (char)rnd.Next(char.MaxValue + 1); }
                    while (c == Value1);
                    return c;
                case CompareMethod.MinMax:
                    if (Value1 > Value2)
                        throw new ArgumentException();
                    return (char)rnd.Next(Value1, Value2 + 1);
                case CompareMethod.NotMinMax:
                    int ri = rnd.Next(Value1 - char.MinValue + char.MaxValue - Value2);
                    if (ri < Value1)
                        return (char)ri;
                    else
                        return (char)(Value2 + ri - Value1 + char.MinValue);
                case CompareMethod.Select:
                    if (Select == null || Select.Length == 0)
                        return false;
                    return Select[rnd.Next(Select.Length)];
                case CompareMethod.NotSelect:
                    if (Select == null || Select.Length == 0)
                        goto case CompareMethod.Any;
                    while (true)
                    {   
                        c = (char)rnd.Next(char.MaxValue + 1);
                        for (int i = 0; i < Select.Length; i++)
                            if (Select[i] == c)
                                continue;
                        return c;
                    }
                case CompareMethod.Special:
                    int samplesTotal, r;
                    switch (Value1)
                    {
                        case 'w':
                            samplesTotal = 63;
                            r = rnd.Next(samplesTotal);
                            if (r < 10)
                                return (char)('0' + r);
                            else if (r < 36)
                                return (char)('A' + r - 10);
                            else if (r < 62)
                                return (char)('a' + r - 36);
                            else
                                return '_';
                        case 'W':
                            while (true)
                            {
                                c = (char)rnd.Next(char.MaxValue + 1);
                                if ((c < 'A' || c > 'Z') &&
                                    (c < 'a' || c > 'z') &&
                                    (c < '0' || c > '9') &&
                                    c != '_')
                                    return c;
                            }
                        case 's':
                            samplesTotal = 5;
                            r = rnd.Next(samplesTotal);
                            switch (r)
                            {
                                case 0:
                                    return ' ';
                                case 1:
                                    return '\t';
                                case 2:
                                    return '\n';
                                case 3:
                                    return '\f';
                                case 4:
                                    return '\r';
                                default:
                                    throw new Exception();
                            }
                        case 'S':
                            while (true)
                            {
                                c = (char)rnd.Next(char.MaxValue + 1);
                                if (c != ' ' && c != '\t' &&
                                    c != '\n' && c != '\f' &&
                                    c != '\r')
                                    return c;
                            }
                        case 'd':
                            samplesTotal = 10;
                            r = rnd.Next(samplesTotal);
                            return r + '0';
                        case 'D':
                            while (true)
                            {
                                c = (char)rnd.Next(char.MaxValue + 1);
                                if (c < '0' || c > '9')
                                    return c;
                            }
                        default:
                            throw new ArgumentOutOfRangeException(nameof(Value1));
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(CompareMethod));
            }
        }
    }
}
