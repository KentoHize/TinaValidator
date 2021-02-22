using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;

namespace Aritiafel.Artifacts.TinaValidator.Serialization
{
    public class AreaJsonConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {   
            if(typeToConvert == typeof(Area) || typeToConvert.IsSubclassOf(typeof(Area)))            
                return true;
            return false;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return (JsonConverter)Activator.CreateInstance(
                typeof(AreaJsonConverterInner<>).MakeGenericType(new Type[] { typeToConvert }),
                BindingFlags.Instance | BindingFlags.Public, null, new object[] { options }, null); ;
        }

        private class AreaJsonConverterInner<T> : JsonConverter<T> where T : Area
        {        
            public AreaJsonConverterInner(JsonSerializerOptions options)
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
                writer.WriteStartObject();
                Type valueType = value.GetType();              
                PropertyInfo[] pis = valueType.GetProperties();
                foreach (PropertyInfo pi in pis)
                {
                    if (pi.GetAccessors(true)[0].IsStatic)
                        continue;
                    else if (pi.Name == "InitialStatus")
                    {
                        writer.WritePropertyName(pi.Name);
                        writer.WriteStartObject();
                        writer.WriteString("ID", (pi.GetValue(value) as TNode).ID);
                        writer.WriteEndObject();
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
