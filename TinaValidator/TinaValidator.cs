using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class TinaValidator
    {
        public const int Invalid = -1;
        public ValidateLogic Logic { get; set; }
        public TinaValidator(ValidateLogic logic = null)
        {
            Logic = logic;
        }

        //private Dictionary<string, object> ParamArray

        // line index etc..
        public string Message { get; set; }
        public bool Validate(List<object> things)
        {
            if (things == null)
                throw new ArgumentNullException("things");

            return AreaStatusChoice(things, 0, Logic.InitialStatus, null) != Invalid;
        }

        public bool Validate(object[] things)
           => Validate(things.ToList());

        private int AreaStatusChoice(List<object> things, int index, Status st, AreaPart ap)
        {
            if (st == Status.EndStatus)
                return index;

            int nextIndex;
            for (int i = 0; i < st.Choices.Count; i++)
            {   
                if(st.Choices[i] is SkipPart)
                    nextIndex = index;
                else if (st.Choices[i] is AreaPart nap)
                    nextIndex = AreaStatusChoice(things, index, nap.Area.InitialStatus, nap);
                else
                {
                    if (index == things.Count)
                        continue;
                    else
                        nextIndex = st.Choices[i].Validate(things, index);
                }
                if (nextIndex != Invalid)
                    return AreaStatusChoice(things, nextIndex, st.Choices[i].NextStatus, ap);
            }
            return Invalid;
        }

        public string CreateRandomToString()
        {
            StringBuilder sb = new StringBuilder();
            List<object> randomList = CreateRandom();
            for (int i = 0; i < randomList.Count; i++)
                sb.Append(randomList[i]);
            return sb.ToString();
        }

        public List<object> CreateRandom()
        {
            List<object> result = new List<object>();
            AreaGetRandom(result, Logic.InitialStatus, null);
            return result;
        }

        private void AreaGetRandom(List<object> result, Status st, AreaPart ap)
        {
            if (st == Status.EndStatus)
                return;
            Random rnd = new Random((int)DateTime.Now.Ticks);
            int choiceIndex = rnd.Next(st.Choices.Count);
            if (st.Choices[choiceIndex] is AreaPart nap)
                AreaGetRandom(result, nap.Area.InitialStatus, nap);
            else
                result.AddRange(st.Choices[choiceIndex].Random());
            AreaGetRandom(result, st.Choices[choiceIndex].NextStatus, ap);
        }
    }
}
