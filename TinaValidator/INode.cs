using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public interface INode
    {
        List<object> Random();
        int Compare(List<object> thing, int startIndex);
    }
}
