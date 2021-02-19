using System;

namespace Aritiafel.Artifacts.Calculator
{
    public interface IObject
    {
        ObjectConst GetObject(IVariableLinker vl);
        Type GetObjectType();        
    }
}
