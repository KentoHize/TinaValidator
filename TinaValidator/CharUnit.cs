using System;
using System.Collections.Generic;
using Aritiafel.Artifacts.Calculator;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class CharUnit : Unit
    {
        public static CharUnit HexadecimalDigit => new CharUnit { CompareMethod = CompareMethod.Special, Value1 = new CharConst('h') };
        public static CharUnit NotHexadecimalDigit => new CharUnit { CompareMethod = CompareMethod.Special, Value1 = new CharConst('H') };
        public static CharUnit Letter => new CharUnit { CompareMethod = CompareMethod.Special, Value1 = new CharConst('w') };
        public static CharUnit NotLetter => new CharUnit { CompareMethod = CompareMethod.Special, Value1 = new CharConst('W') };
        public static CharUnit Space => new CharUnit { CompareMethod = CompareMethod.Special, Value1 = new CharConst('s') };
        public static CharUnit NotSpace => new CharUnit { CompareMethod = CompareMethod.Special, Value1 = new CharConst('S') };
        public static CharUnit Digit => new CharUnit { CompareMethod = CompareMethod.Special, Value1 = new CharConst('d') };
        public static CharUnit NotDigit => new CharUnit { CompareMethod = CompareMethod.Special, Value1 = new CharConst('D') };
        public CompareMethod CompareMethod { get; set; }
        public INumber Value1 { get; set; } //min exact
        public INumber Value2 { get; set; } //max
        public INumber[] Select
        {
            get => _Select;
            set
            {
                if (value != null)
                {
                    for (long i = 0; i < value.Length; i++)
                        if (!(value[i] is CharConst || value[i] is CharVar))
                            throw new ArgumentException(nameof(Select));
                }
                _Select = value;
            }
        }
        private INumber[] _Select;
        public CharUnit()
            : this(CompareMethod.Any)
        { }
        public CharUnit(CompareMethod compareMethod = CompareMethod.Any)
        {
            CompareMethod = compareMethod;
        }
        
        public CharUnit(char exactValue, CompareMethod compareMethod = CompareMethod.Exact)
        {
            CompareMethod = compareMethod;
            Value1 = new CharConst(exactValue);
        }
        public CharUnit(char minValue, char maxValue, CompareMethod compareMethod = CompareMethod.MinMax)
        {
            CompareMethod = compareMethod;
            Value1 = new CharConst(minValue);
            Value2 = new CharConst(maxValue);
        }

        public CharUnit(string selectChars, CompareMethod compareMethod = CompareMethod.Select)
            : this(selectChars.ToCharArray(), compareMethod)
        { }
        public CharUnit(char[] select, CompareMethod compareMethod = CompareMethod.Select)
        {
            CompareMethod = compareMethod;
            INumber[] array = new INumber[select.Length];
            for (int i = 0; i < select.Length; i++)
                array[i] = new CharConst(select[i]);
            Select = array;
        }

        public override bool Compare(object o, IVariableLinker vl)
        {
            if (!(o is CharConst c))
                return false;
            switch (CompareMethod)
            {
                case CompareMethod.Any:
                    return true;
                case CompareMethod.Exact:
                    return Value1.GetResult(vl) == c;
                case CompareMethod.Not:
                    return Value1.GetResult(vl) != c;
                case CompareMethod.MinMax:
                    return c >= Value1.GetResult(vl) && c <= Value2.GetResult(vl);
                case CompareMethod.NotMinMax:
                    return c < Value1.GetResult(vl) || c > Value2.GetResult(vl);
                case CompareMethod.Select:
                    if (Select == null)
                        return false;
                    for (int i = 0; i < Select.Length; i++)
                        if (Select[i].GetResult(vl) == c)
                            return true;
                    return false;
                case CompareMethod.NotSelect:
                    if (Select == null)
                        return true;
                    for (int i = 0; i < Select.Length; i++)
                        if (Select[i].GetResult(vl) == c)
                            return false;
                    return true;
                case CompareMethod.Special:
                    switch (Value1.GetResult(vl).Value)
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
                        case 'h':
                            return (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') ||
                                (c >= 'A' && c <= 'F');
                        case 'H':
                            return (c < '0' || c > '9') && (c < 'a' || c > 'f') &&
                                (c < 'A' || c > 'F');
                        default:
                            throw new ArgumentOutOfRangeException(nameof(Value1));
                    }
                default:
                    throw new ArgumentOutOfRangeException(nameof(CompareMethod));
            }
        }

        public override IObject Random(IVariableLinker vl)
        {
            if(CompareMethod == CompareMethod.Exact)
                return Value1;
            Random rnd = new Random((int)DateTime.Now.Ticks);
            char c;
            switch (CompareMethod)
            {       
                case CompareMethod.Any:
                    return new CharConst((char)rnd.Next(char.MaxValue + 1));
                case CompareMethod.Not:
                    do { c = (char)rnd.Next(char.MaxValue + 1); }
                    while (c == (char)Value1.GetResult(vl).Value);
                    return new CharConst(c);
                case CompareMethod.MinMax:
                    if (Value1.GetResult(vl) > Value2.GetResult(vl))
                        throw new ArgumentException();
                    return new CharConst((char)rnd.Next((char)Value1.GetResult(vl).Value,
                        (char)Value2.GetResult(vl).Value + 1));
                case CompareMethod.NotMinMax:
                    int ri = rnd.Next((char)Value1.GetResult(vl).Value - char.MinValue + char.MaxValue - (char)Value2.GetResult(vl).Value);
                    if (ri < (char)Value1.GetResult(vl).Value)
                        return new CharConst((char)ri);
                    else
                        return new CharConst((char)((char)Value2.GetResult(vl).Value + ri - 
                            (char)Value1.GetResult(vl).Value + char.MinValue));
                case CompareMethod.Select:
                    if (Select == null || Select.Length == 0)
                        throw new ArgumentNullException(nameof(Select));
                    return Select[rnd.Next(Select.Length)];
                case CompareMethod.NotSelect:
                    if (Select == null || Select.Length == 0)
                        goto case CompareMethod.Any;
                    while (true)
                    {   
                        c = (char)rnd.Next(char.MaxValue + 1);
                        for (int i = 0; i < Select.Length; i++)
                            if ((char)Select[i].GetResult(vl).Value == c)
                                continue;
                        return new CharConst(c);
                    }
                case CompareMethod.Special:
                    int samplesTotal, r;
                    switch (Value1.GetResult(vl).Value)
                    {
                        case 'w':
                            samplesTotal = 63;
                            r = rnd.Next(samplesTotal);
                            if (r < 10)
                                return new CharConst((char)('0' + r));
                            else if (r < 36)
                                return new CharConst((char)('A' + r - 10));
                            else if (r < 62)
                                return new CharConst((char)('a' + r - 36));
                            else
                                return new CharConst('_');
                        case 'W':
                            while (true)
                            {
                                c = (char)rnd.Next(char.MaxValue + 1);
                                if ((c < 'A' || c > 'Z') &&
                                    (c < 'a' || c > 'z') &&
                                    (c < '0' || c > '9') &&
                                    c != '_')
                                    return new CharConst(c);
                            }
                        case 's':
                            samplesTotal = 5;
                            r = rnd.Next(samplesTotal);
                            switch (r)
                            {
                                case 0:
                                    return new CharConst(' ');
                                case 1:
                                    return new CharConst('\t');
                                case 2:
                                    return new CharConst('\n');
                                case 3:
                                    return new CharConst('\f');
                                case 4:
                                    return new CharConst('\r');
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
                                    return new CharConst(c);
                            }
                        case 'd':
                            samplesTotal = 10;
                            r = rnd.Next(samplesTotal);
                            return new CharConst((char)(r + '0'));
                        case 'D':
                            while (true)
                            {
                                c = (char)rnd.Next(char.MaxValue + 1);
                                if (c < '0' || c > '9')
                                    return new CharConst(c);
                            }
                        case 'h':
                            samplesTotal = 16;
                            r = rnd.Next(samplesTotal);
                            if (r < 10)
                                return new CharConst((char)(r + '0'));
                            return new CharConst((char)(r + 'a' - 10));
                        case 'H':
                            while (true)
                            {
                                c = (char)rnd.Next(char.MaxValue + 1);
                                if ((c < '0' || c > '9') && (c < 'a' || c > 'f') &&
                                    (c < 'A' || c > 'F'))
                                    return new CharConst(c);
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
