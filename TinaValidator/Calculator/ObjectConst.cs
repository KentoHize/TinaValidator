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
        public abstract BooleanConst NotEqualTo(ObjectConst b);
        public static BooleanConst operator ==(ObjectConst a, ObjectConst b)
            => a.EqualTo(b);
        public static BooleanConst operator !=(ObjectConst a, ObjectConst b)
            => a.NotEqualTo(b);

    }
}
