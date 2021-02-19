using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public interface IString
    {
        StringConst GetResult(IVariableLinker vl);
    }
}
