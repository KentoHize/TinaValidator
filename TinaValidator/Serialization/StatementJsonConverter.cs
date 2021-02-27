using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;
using Aritiafel.Artifacts.Calculator;
using Aritiafel.Locations.StorageHouse;

namespace Aritiafel.Artifacts.TinaValidator.Serialization
{
    public class StatementJsonConverter : DefaultJsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
            => typeof(Statement).IsAssignableFrom(typeToConvert);
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
            => (JsonConverter)Activator.CreateInstance(
                typeof(StatementJsonConverterInner<>).MakeGenericType(new Type[] { typeToConvert }),
                BindingFlags.Instance | BindingFlags.Public, null, Array.Empty<object>(), null);
        private class StatementJsonConverterInner<T> : DefaultJsonConverter<T> where T : Statement
        {
            public StatementJsonConverterInner()
            { }

            public override void SetPropertyValue(string propertyName, object instance, object value)
            {
                if (instance.GetType().GetProperty(propertyName).PropertyType == typeof(Type))
                    value = value != null ? Type.GetType(value.ToString()) : null;
                base.SetPropertyValue(propertyName, instance, value);
            }

            public override object GetPropertyValueAndWrite(string propertyName, object instance, bool skip = false)
            {
                if (instance.GetType().GetProperty(propertyName).PropertyType == typeof(Type))
                    return ((Type)base.GetPropertyValueAndWrite(propertyName, instance, skip)).Name;
                return base.GetPropertyValueAndWrite(propertyName, instance, skip);
            }     
        }
    }
}
