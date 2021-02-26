using System;

namespace Aritiafel.Artifacts.Calculator
{
    public interface IObject : ICloneable
    {
        ObjectConst GetObject(IVariableLinker vl);
        Type GetObjectType();
    }
}
