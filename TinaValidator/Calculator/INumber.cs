using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public interface INumber
    {
        NumberConst GetResult(IVariableLinker vl);
    }
}
