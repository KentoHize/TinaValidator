using System;

namespace Aritiafel.Artifacts.Calculator
{
    public abstract class Expression : IObject
    {
        public abstract object Clone();
        public abstract ObjectConst GetObject(IVariableLinker vl);
        public abstract Type GetObjectType();
    }
}
