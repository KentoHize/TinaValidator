using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public interface IBoolean
    {
        BooleanConst GetResult(IVariableLinker vl);
    }
}
