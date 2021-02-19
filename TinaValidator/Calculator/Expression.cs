using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public abstract class Expression : IObject
    {
        public abstract ObjectConst GetObject(IVariableLinker vl);
        public abstract Type GetObjectType();
    }
}
