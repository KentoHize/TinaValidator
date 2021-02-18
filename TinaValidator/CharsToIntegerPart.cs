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

        public override int Validate(List<object> thing, int startIndex = 0)
        {
            StringBuilder sb = new StringBuilder();
            int i;
            for (i = 0; startIndex + i < thing.Count; i++)
            {
                if (!(thing[startIndex + i] is char))
                    break;
                else if (!char.IsDigit((char)thing[startIndex + i]) && ((char)thing[startIndex + i] != '-' || i != 0))
                    break;
                else if (i > 29) // decimal max length + '-'
                    break;
                sb.Append(thing[startIndex + i]);
            }

            if (!decimal.TryParse(sb.ToString(), out decimal d))
                return -1;

            IntegerUnit iu = new IntegerUnit(this);
            return iu.Compare(d) ? startIndex + i : -1;
        }

        public override List<object> Random()
        {
            IntegerUnit iu = new IntegerUnit(this);
            return iu.Random().ToString().ToObjectList();
        }
    }
}
