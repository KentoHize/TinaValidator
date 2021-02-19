using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public enum Operator
    {
        None = 0,
        Plus,
        Minus,
        Multiply,
        Divide,
        ExactlyDivide,
        Remainder,
        PlusOne,
        MinusOne,
        Concat,
        GreaterThan,
        GreaterThanOrEqualTo,
        LessThan,
        LessThanOrEqualTo,
        EqualTo,
        NotEqualTo,
        And,
        Or,
        Not
    }
}
