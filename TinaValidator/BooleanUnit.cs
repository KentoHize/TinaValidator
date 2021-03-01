using System;
using Aritiafel.Artifacts.Calculator;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class BooleanUnit : Unit
    {
        public static BooleanUnit True => new BooleanUnit(true);
        public static BooleanUnit False => new BooleanUnit(false);
        public CompareMethod CompareMethod { get; set; }
        public IBoolean Value { get; set; }
        public BooleanUnit()
            : this(CompareMethod.Any)
        { }
        public BooleanUnit(CompareMethod compareMethod = CompareMethod.Any)
            => CompareMethod = CompareMethod.Any;
        public BooleanUnit(CharsToBooleanPart ctbp)
        {
            CompareMethod = ctbp.CompareMethod;
            Value = ctbp.Value;
        }
        public BooleanUnit(bool value, CompareMethod compareMethod = CompareMethod.Exact)
            : this(new BooleanConst(value) as IBoolean, compareMethod)
        { }
        public BooleanUnit(BooleanConst value, CompareMethod compareMethod = CompareMethod.Exact)
            : this(value as IBoolean, compareMethod)
        { }
        public BooleanUnit(IBoolean value, CompareMethod compareMethod = CompareMethod.Exact)
        {
            CompareMethod = compareMethod;
            Value = value;
        }

        public override bool Compare(ObjectConst o, IVariableLinker vl = null)
        {
            if (!(o is BooleanConst b))
                return false;

            switch (CompareMethod)
            {
                case CompareMethod.Any:
                    return true;
                case CompareMethod.Exact:
                    return b == Value.GetResult(vl);
                case CompareMethod.Not:
                    return b != Value.GetResult(vl);
                default:
                    throw new ArgumentOutOfRangeException(nameof(CompareMethod));
            }
        }

        public override ObjectConst Random(IVariableLinker vl = null)
        {
            if (CompareMethod == CompareMethod.Exact)
                return Value.GetResult(vl);

            Random rnd = new Random((int)DateTime.Now.Ticks);
            switch (CompareMethod)
            {
                case CompareMethod.Any:
                    return new BooleanConst(rnd.Next(2) == 1);
                case CompareMethod.Not:
                    return !Value.GetResult(vl);
                default:
                    throw new ArgumentOutOfRangeException(nameof(CompareMethod));
            }
        }
    }
}
