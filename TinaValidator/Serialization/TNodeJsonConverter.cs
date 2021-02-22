using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;

namespace Aritiafel.Artifacts.TinaValidator.Serialization
{
    public class TNodeJsonConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if(typeToConvert == typeof(TNode) || typeToConvert.IsSubclassOf(typeof(TNode)))            
                return true;
            return false;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return (JsonConverter)Activator.CreateInstance(
                typeof(TNodeJsonConverterInner<>).MakeGenericType(new Type[] { typeToConvert }),
                BindingFlags.Instance | BindingFlags.Public, null, new object[] { options }, null); ;
        }

        private class TNodeJsonConverterInner<T> : JsonConverter<T> where T : TNode
        {        
            public TNodeJsonConverterInner(JsonSerializerOptions options)
            { }

            public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                
                throw new NotImplementedException();
            }

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                if (value == null)
                {
                    writer.WriteNullValue();
                    return;
                }

                
                    
                Type valueType = value.GetType();
                writer.WriteStartObject();
                PropertyInfo[] pis = valueType.GetProperties();


                //PropertyInfo piID = typeof(T).GetProperty("ID");
                //writer.WriteString("ID", piID.GetValue(value).ToString());
                writer.WriteString("Type", valueType.Name);

                //Console.WriteLine(value.GetType().Name);
                foreach (PropertyInfo pi in pis)
                {

                    if (pi.Name == "ID")
                        continue;
                    else if (pi.GetAccessors(true)[0].IsStatic)
                        continue;
                    else if (pi.PropertyType == typeof(TNode) && pi.GetValue(value) != null)
                    {
                        writer.WriteString(pi.Name, (pi.GetValue(value) as TNode).ID);
                        //writer.WritePropertyName(pi.Name);
                        //writer.WriteStartObject();
                        //writer.WriteString("ID", (pi.GetValue(value) as TNode).ID);                        
                        //writer.WriteEndObject();
                        //Console.WriteLine(pi.Name);
                        //writer.WriteString(pi.Name, ((TNode)pi.GetValue(value)).ID);
                    }
                    else if (pi.PropertyType == typeof(Area))
                    { }                    
                    else
                    {   
                        JsonConverter jc = options.GetConverter(pi.PropertyType);
                        writer.WritePropertyName(pi.Name);
                        jc.GetType().GetMethod("Write").Invoke(jc, new object[] { writer, pi.GetValue(value), options });
                    }
                }
                writer.WriteEndObject();
            }
        }
    }
}
