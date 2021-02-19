using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public abstract class ObjectConst : IObject
    {
        public abstract ObjectConst GetObject(IVariableLinker vl);
        public abstract Type GetObjectType();
        protected abstract BooleanConst ReverseEqualTo(ObjectConst b);
        public abstract BooleanConst EqualTo(LongConst b);
        public abstract BooleanConst EqualTo(DoubleConst b);
        protected abstract BooleanConst ReverseGreaterThan(ObjectConst b);
        public abstract BooleanConst GreaterThan(LongConst b);
        public abstract BooleanConst GreaterThan(DoubleConst b);
        protected abstract BooleanConst ReverseLessThan(ObjectConst b);
        public abstract BooleanConst LessThan(LongConst b);
        public abstract BooleanConst LessThan(DoubleConst b);
        public static BooleanConst operator ==(ObjectConst a, ObjectConst b)
            => b.ReverseEqualTo(a);
        public static BooleanConst operator !=(ObjectConst a, ObjectConst b)
            => !b.ReverseEqualTo(a);
        public static BooleanConst operator >(ObjectConst a, ObjectConst b)
            => b.ReverseGreaterThan(a);
        public static BooleanConst operator >=(ObjectConst a, ObjectConst b)
            => b.ReverseGreaterThan(a) || b.ReverseEqualTo(a);
        public static BooleanConst operator <(ObjectConst a, ObjectConst b)
            => b.ReverseLessThan(a);
        public static BooleanConst operator <=(ObjectConst a, ObjectConst b)
            => b.ReverseLessThan(a) || b.ReverseEqualTo(a);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;
            if (ReferenceEquals(obj, null))
                return false;
            if (!(obj is ObjectConst o))
                return false;
            return (this == o).Value;
        }
    }
}
