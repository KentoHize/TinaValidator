using System.Collections.Generic;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class TinaValidator
    {
        public ValidateLogic Logic { get; set; }

        // line index etc..
        public string Message { get; set; }

        public bool Validate(List<object> things)
        {
            return false;
        }

        public List<object> CreateRandom()
        {
            return null;
        }
    }
}
