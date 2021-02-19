using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public interface IObject
    {
        ObjectConst GetObject(IVariableLinker vl);
        Type GetObjectType();
    }
}
