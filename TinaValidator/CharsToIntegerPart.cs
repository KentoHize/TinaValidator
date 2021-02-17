using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class CharsToIntegerPart : Part
    {
        public CompareMethod CompareMethod { get; set; }
        public decimal Value1 { get; set; }
        public decimal Value2 { get; set; }
        public CharsToIntegerPart()
            => CompareMethod = CompareMethod.Any;

        public CharsToIntegerPart(IntegerUnit iu)
        {
            CompareMethod = iu.CompareMethod;
            Value1 = iu.Value1;
            Value2 = iu.Value2;
        }

        public CharsToIntegerPart(decimal exactValue)
        {
            CompareMethod = CompareMethod.Exact;
            Value1 = exactValue;
        }

        public CharsToIntegerPart(decimal minValue, decimal maxValue)
        {
            CompareMethod = CompareMethod.MinMax;
            Value1 = minValue;
            Value2 = maxValue;
        }

        public override int Validate(List<object> thing, int startIndex)
        {   
            StringBuilder sb = new StringBuilder();
            int i;
            for(i = 0; startIndex + i < thing.Count; i++)
            {
                if (!(thing[startIndex + i] is char))
                    break;
                else if (!char.IsDigit((char)thing[startIndex + i]) || ((char)thing[startIndex + i] == '-' && i == 0))
                    break;
                else if (i > 28) // decimal max length
                    break;
                sb.Append(thing[startIndex + i]);
                i++;
            }

            if (!decimal.TryParse(sb.ToString(), out decimal d))
                return -1;

            IntegerUnit IU = new IntegerUnit(this);
            return IU.Compare(d) ? startIndex + i : -1;
        }

        public override List<object> Random()
        {
            List<object> result = new List<object>();
            IntegerUnit IU = new IntegerUnit(this);
            char[] ca = IU.Random().ToString().ToCharArray();
            for (int i = 0; i < ca.Length; i++)
                result.Add(ca[i]);
            return result;
        }
    }
}
