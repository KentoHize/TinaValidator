﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    class StringVar : Variable, IString
    {
        public StringConst GetResult(IVariableLinker vl)
            => new StringConst(vl.GetValue(this).ToString());
        public StringVar(string name = null, List<object> keys = null, VariableSource source = VariableSource.CustomVariable)
            : base(name, keys, source)
        { }
    }
}
