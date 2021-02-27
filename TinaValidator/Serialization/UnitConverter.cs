using Aritiafel.Locations.StorageHouse;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aritiafel.Artifacts.TinaValidator.Serialization
{
    public class UnitConverter : DefaultJsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
            => typeof(Unit).IsAssignableFrom(typeToConvert);
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
            => (JsonConverter)Activator.CreateInstance(
                typeof(UnitConverterInner<>).MakeGenericType(new Type[] { typeToConvert }),
                BindingFlags.Instance | BindingFlags.Public, null, Array.Empty<object>(), null);
        private class UnitConverterInner<T> : DefaultJsonConverter<T> where T : Unit
        {
            public UnitConverterInner()
            { }

            public override void SetPropertyValue(string propertyName, object instance, object value)
            {
                //if (instance.GetType().GetProperty(propertyName).PropertyType == typeof(char) && value == null)
                //    value = '\0';
                base.SetPropertyValue(propertyName, instance, value);
            }

            public override object GetPropertyValueAndWrite(string propertyName, object instance, bool skip = false)
            {
                object value = base.GetPropertyValueAndWrite(propertyName, instance, skip);
                //if (value is char c)
                //    if (c == '\0')
                //        return null;
                return value;
            }

        }
    }
}