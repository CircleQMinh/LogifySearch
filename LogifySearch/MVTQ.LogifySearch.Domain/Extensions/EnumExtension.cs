using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVTQ.LogifySearch.Domain.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum value)
        {
            if (value == null)
            {
                return "";
            }
            var field = value.GetType().GetField(value.ToString());
            if (field == null)
            {
                return "";
            }
            var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attr != null ? attr.Description : "";
        }
    }
}
