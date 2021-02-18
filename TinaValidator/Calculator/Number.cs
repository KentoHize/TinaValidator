using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public abstract class Number
    {
        public abstract NumberConst GetResult(IVariableLinker vl);
    }
}
