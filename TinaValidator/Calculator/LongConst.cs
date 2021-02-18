using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.Calculator
{
    public class LongConst : NumberConst
    {
        public static NumberConst operator+(LongConst a, LongConst b)
            => new LongConst(a.Value + b.Value);

        public override string ToString()
            => Value.ToString();

        public object GetValue()
            => Value;

        public LongConst(long value)
        {
            Value = value;
        }
        private long Value { get; set; }
    }
}
