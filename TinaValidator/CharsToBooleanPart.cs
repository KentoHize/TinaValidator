using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class CharsToBooleanPart : Part
    {
        public CompareMethod CompareMethod { get; set; }
        public bool Value { get; set; }        
        public CharsToBooleanPart()
            : this(CompareMethod.Any)
        { }
        public CharsToBooleanPart(CompareMethod compareMethod = CompareMethod.Any)
            => CompareMethod = compareMethod;
        public CharsToBooleanPart(BooleanUnit bu)
        {
            CompareMethod = bu.CompareMethod;
            Value = bu.Value;
        }
        public CharsToBooleanPart(bool value, CompareMethod compareMethod = CompareMethod.Exact)
        {
            CompareMethod = compareMethod;
            Value = value;
        }

        public override int Validate(List<object> thing, int startIndex = 0)
        {
            bool? result = null;

            if (startIndex + 3 < thing.Count && thing[startIndex] is char c1 &&
               thing[startIndex + 1] is char c2 && thing[startIndex + 2] is char c3 &&
               thing[startIndex + 3] is char c4)
            {
                if (char.ToUpper(c1) == 'T' && char.ToUpper(c2) == 'R' &&
                    char.ToUpper(c3) == 'U' && char.ToUpper(c4) == 'E')
                    result = true;
                else if (startIndex + 4 < thing.Count && thing[startIndex + 4] is char c5 &&
                char.ToUpper(c1) == 'F' && char.ToUpper(c2) == 'A' && char.ToUpper(c3) == 'L' &&
                char.ToUpper(c4) == 'S' && char.ToUpper(c5) == 'E')
                    result = false;
            }

            if (result == null)
                return -1;

            BooleanUnit bu = new BooleanUnit(this);
            if (!bu.Compare(result))
                return -1;
            return (bool)result ? startIndex + 4 : startIndex + 5;
        }

        public override List<object> Random()
        {
            BooleanUnit bu = new BooleanUnit(this);
            return bu.Random().ToString().ToLower().ToObjectList();
        }
    }
}
