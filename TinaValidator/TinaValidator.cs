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
                throw new ArgumentNullException(nameof(things));

            return AreaNodeSelection(things, 0, Logic.InitialStatus, null) != Invalid;
        }

        public bool Validate(object[] things)
           => Validate(things.ToList());

        private int AreaNodeSelection(List<object> things, int index, TNode node, AreaStart ap)
        {
            int nextIndex = Invalid;
            switch(node)
            {
                case EndNode _:
                    return index;
                case AreaStart ars:
                    nextIndex = AreaNodeSelection(things, index, ars.Area.InitialStatus, ars);
                    break;
                case Part p:
                    if (index == things.Count)
                        break;
                    else
                        nextIndex = p.Validate(things, index);
                    break;
                case Status st:
                    for (int i = 0; i < st.Choices.Count; i++)
                    { 
                        nextIndex = AreaNodeSelection(things, index, st.Choices[i], ap);
                        if (nextIndex != Invalid)
                            return nextIndex;
                    }
                    return Invalid;
            }
            if (nextIndex != Invalid)
                return AreaNodeSelection(things, nextIndex, node.NextNode, ap);
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

        private void AreaGetRandom(List<object> result, TNode node, AreaStart ap)
        {
            switch (node)
            {
                case EndNode _:
                    return;
                case AreaStart ars:
                    AreaGetRandom(result, ars.Area.InitialStatus, ars);
                    AreaGetRandom(result, ars.NextNode, ap);
                    break;
                case Part p:
                    result.AddRange(p.Random());
                    AreaGetRandom(result, p.NextNode, ap);
                    break;
                case Status st:
                    Random rnd = new Random((int)DateTime.Now.Ticks);
                    int choiceIndex = rnd.Next(st.Choices.Count);
                    AreaGetRandom(result, st.Choices[choiceIndex], ap);
                    break;
            }
        }
    }
}
