using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moonlight.Conversion.Converters
{
    public class ListConverter : Converter<IList>
    {
        public override bool IsGeneric => true;

        protected override IList ToObject(string value, Type type, IConversionFactory factory)
        {
            Type generic = type.GenericTypeArguments.FirstOrDefault();
            Type listType = typeof(List<>).MakeGenericType(type);
            var list = (IList)Activator.CreateInstance(listType);

            string[] values = value.Split(' ');

            foreach (string entry in values)
            {
                list.Add(factory.ToObject(entry, generic));
            }

            return list;
        }

        protected override string ToString(IList value, Type type, IConversionFactory factory)
        {
            Type valueType = value.GetType().GenericTypeArguments.FirstOrDefault();
            var sb = new StringBuilder();
            foreach (object entry in value)
            {
                sb.Append(factory.ToString(entry, valueType)).Append(" ");
            }

            return sb.ToString().TrimEnd();
        }
    }
}