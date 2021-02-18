using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public class LongVar : NumberVar
    {
        public override NumberConst GetResult(IVariableLinker vl)
            => new LongConst((long)vl.GetValue(this));
    }
}
