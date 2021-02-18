using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public enum VariableForm
    {
        EnvoirmentVariable = 0,
        CustomVariable
    }

    public class Variable
    {
        public VariableForm VariableForm { get; set; }
        public string VariableName { get; set; }
        public List<object> Keys { get; set; }
        public object Tag { get; set; }

        //public override object GetValue(IVariableLinker vr);

    }
}
