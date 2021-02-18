using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public class DoubleVar : NumberVar
    {
        public override NumberConst GetResult(IVariableLinker vl)
            => new DoubleConst((double)vl.GetValue(this));
    }
}
