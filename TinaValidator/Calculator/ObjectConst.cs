using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public abstract class ObjectConst : IObject
    {
        public abstract ObjectConst GetObject(IVariableLinker vl);
        public abstract Type GetObjectType();
        public abstract BooleanConst EqualTo(ObjectConst b);
        public abstract BooleanConst GreaterThan(ObjectConst b);
        public abstract BooleanConst LessThan(ObjectConst b);
        public static BooleanConst operator ==(ObjectConst a, ObjectConst b)
            => a.EqualTo(b);
        public static BooleanConst operator !=(ObjectConst a, ObjectConst b)
            => !a.EqualTo(b);
        public static BooleanConst operator >(ObjectConst a, ObjectConst b)
            => a.GreaterThan(b);
        public static BooleanConst operator >=(ObjectConst a, ObjectConst b)
            => a.GreaterThan(b) || a.EqualTo(b);
        public static BooleanConst operator <(ObjectConst a, ObjectConst b)
            => a.LessThan(b);
        public static BooleanConst operator <=(ObjectConst a, ObjectConst b)
            => a.LessThan(b) || a.EqualTo(b);

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
