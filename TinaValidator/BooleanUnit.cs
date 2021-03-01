using System;
using Aritiafel.Artifacts.Calculator;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class BooleanUnit : Unit
    {
        public static BooleanUnit True => new BooleanUnit(true);
        public static BooleanUnit False => new BooleanUnit(false);
        public CompareMethod CompareMethod { get; set; }
        public bool Value { get; set; }
        public BooleanUnit()
            : this(CompareMethod.Any)
        { }
        public BooleanUnit(CompareMethod compareMethod = CompareMethod.Any)
        {
            CompareMethod = CompareMethod.Any;
        }

        public BooleanUnit(CharsToBooleanPart ctbp)
        {
            CompareMethod = ctbp.CompareMethod;
            Value = ctbp.Value;
        }

        public BooleanUnit(bool value, CompareMethod compareMethod = CompareMethod.Exact)
        {
            CompareMethod = compareMethod;
            Value = value;
        }

        public override bool Compare(object o, IVariableLinker variableLinker)
        {
            if (!(o is bool b))
                return false;

            switch (CompareMethod)
            {
                case CompareMethod.Any:
                    return true;
                case CompareMethod.Exact:
                    return b == Value;
                case CompareMethod.Not:
                    return b != Value;
                default:
                    throw new ArgumentOutOfRangeException(nameof(CompareMethod));
            }
        }

        public override object Random(IVariableLinker variableLinker)
        {
            if (CompareMethod == CompareMethod.Exact)
                return Value;

            Random rnd = new Random((int)DateTime.Now.Ticks);
            switch (CompareMethod)
            {
                case CompareMethod.Any:
                    return rnd.Next(2) == 1;
                case CompareMethod.Not:
                    return !Value;
                default:
                    throw new ArgumentOutOfRangeException(nameof(CompareMethod));
            }
        }
    }
}
