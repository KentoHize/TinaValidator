using System;
using System.Collections.Generic;
using System.Linq;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class TinaValidator
    {
        public ValidateLogic Logic { get; set; }
        public TinaValidator(ValidateLogic logic = null)
        {
            Logic = logic;
        }

        // line index etc..
        public string Message { get; set; }
        public bool Validate(List<object> things)
        {
            if (things == null)
                throw new ArgumentNullException("things");
            skip = false;
            return false;
            //return StatusChoice(things, 0, Logic.InitialStatus);
        }

        public bool Validate(object[] things)        
           => Validate(things.ToList());

        private bool skip;
        //private bool StatusChoice(object[] things, int index, Status st)
        //{
        //    if (st == Logic.EndStatus)
        //        return true;
        //    if (index == things.Length - 1)
        //        return false;
            
        //    for(int i = 0; i < st.Choices.Count; i++)
        //    {
        //        st.Choices[i].Compare(things);
        //    }
            
        //    // To DO
        //    return false;
        //}

        public List<object> CreateRandom()
        {
            return null;
        }
    }
}
