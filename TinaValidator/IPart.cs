using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public interface IPart
    {
        List<object> Random();
        bool Compare(List<object> b);
    }
}
