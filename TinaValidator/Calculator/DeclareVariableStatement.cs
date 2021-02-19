using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public class DeclareVariableStatement : Statement
    {
        public string VariableName { get; set; }
        public Type Type { get; set; }
        public byte Dimension { get; set; }
        public List<int> Count { get; set; }
        public IObject InitialValue { get; set; }
    }
}
