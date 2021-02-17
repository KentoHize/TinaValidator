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

            return StatusChoice(things, 0, Logic.InitialStatus);
        }

        public bool Validate(object[] things)
           => Validate(things.ToList());

        //private bool skip;
        private bool StatusChoice(List<object> things, int index, Status st)
        {
            if (st == Logic.EndStatus && index == things.Count)
                return true;
            if (index == things.Count)
                return false;

            for(int i = 0; i < st.Choices.Count; i++)
            {
                int nextIndex = st.Choices[i].Validate(things, index);                
                if(nextIndex != -1)
                    if(StatusChoice(things, nextIndex, st.Choices[i].NextStatus))
                        return true;
            }
            return false;
        }

        public List<object> CreateRandom()
        {
            List<object> result = new List<object>();
            GetRandom(result, Logic.InitialStatus);
            return result;
        }

        private void GetRandom(List<object> result, Status st)
        {
            if (st == Logic.EndStatus)
                return;
            Random rnd = new Random((int)DateTime.Now.Ticks);
            int choiceIndex = rnd.Next(st.Choices.Count);
            result.AddRange(st.Choices[choiceIndex].Random());
            GetRandom(result, st.Choices[choiceIndex].NextStatus);
        }
    }
}
