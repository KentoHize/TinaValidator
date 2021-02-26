using Aritiafel.Artifacts.Calculator;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aritiafel.Artifacts.TinaValidator
{
    public class TVMemory : IMemory
    {
        Dictionary<string, object> Variables { get; set; } = new Dictionary<string, object>();
        Dictionary<string, Type> VariablesType { get; set; } = new Dictionary<string, Type>();
        private object CreateArray(byte dimensions, int[] counts, byte dimensionIndex = 0)
        {   
            object[] result = new object[counts[dimensionIndex]];
            if (dimensions <= 0)
                return result;
            for (int i = 0; i < counts[dimensionIndex]; i++)
                result[0] = CreateArray(--dimensions, counts, ++dimensionIndex);
            return result;
        }
        public object GetValue(Variable v)
        {
            if (!Variables.ContainsKey(v.Name))
                throw new KeyNotFoundException();
            if (v.Keys == null)
                return Variables[v.Name];
            else
            {
                object[] o = Variables[v.Name] as object[];
                for (int i = 0; i < v.Keys.Count - 1; i++)
                    o = o[(int)v.Keys[i]] as object[];
                return o[(int)v.Keys[v.Keys.Count - 1]];
            }
        }

        public void SetValue(Variable v, IObject value)
        {
            if (!Variables.ContainsKey(v.Name))
                throw new KeyNotFoundException();
            if (v.Keys == null)
                Variables[v.Name] = value.GetObject(this);
            else
            {
                object[] o = Variables[v.Name] as object[];
                for (int i = 0; i < v.Keys.Count - 1; i++)
                    o = o[(int)v.Keys[i]] as object[];
                o[(int)v.Keys[v.Keys.Count - 1]] = value;
            }
        }

        public bool DeclareVariable(string name, Type type, byte dimensions = 0, int[] counts = null, IObject initialValue = null)
        {
            if (dimensions > 10 || dimensions < 0)
                throw new ArgumentOutOfRangeException(nameof(dimensions));
            if (Variables.ContainsKey(name))
                return false;

            if (dimensions == 0)
            {
                Variables.Add(name, initialValue?.GetObject(this));
                VariablesType.Add(name, type);
            }
            else
            {
                object array = CreateArray(dimensions, counts);
                Variables.Add(name, array);
                VariablesType.Add(name, type);
            }
            return true;
        }

        public bool DeleteVariable(string name)
        {
            if (!Variables.ContainsKey(name))
                return false;
            Variables.Remove(name);
            VariablesType.Remove(name);
            return true;
        }

        public void ClearVariables()
        {
            Variables.Clear();
            VariablesType.Clear();
        }

        public TVMemory()
        { }

        public TVMemory(TVMemory memory)
        {
            memory.Variables = new Dictionary<string, object>(Variables);
            memory.VariablesType = new Dictionary<string, Type>(VariablesType);
            //To Do, Really Copy?
        }
    }
}
