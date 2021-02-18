using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public enum TypeList
    {
        None = 0,
        Boolean,
        Char,
        Integer,
        Long,
        Double,
        String,
        Array,
        Object
    }

    public enum Operator
    {
        None = 0,
        Plus,
        Minus,
        Multiply,
        Divide,
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
