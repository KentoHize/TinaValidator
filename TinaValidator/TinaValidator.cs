using System;
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
            if (things == null)
                throw new ArgumentNullException("things");
            return StatusChoice(things, 0, Logic.InitialStatus);
        }

        private bool StatusChoice(List<object> things, int index, Status st)
        {
            if (st == Logic.EndStatus)
                return true;
            if (index == things.Count - 1)
                return false;

            // To DO
            return false;
        }

        public List<object> CreateRandom()
        {
            return null;
        }
    }
}
