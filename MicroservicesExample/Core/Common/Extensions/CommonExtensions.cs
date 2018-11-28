using System.Collections.Generic;
using System.Reflection;

namespace MicroservicesExample.Core.Common.Extensions
{
    public static class CommonExtensions
    {
        public static IDictionary<string, string> ToDictionary(this object obj)
        {
            var dict = new Dictionary<string, string>();

            if (obj != null)
            {
                foreach (var property in obj.GetType().GetProperties())
                {
                    dict.Add(property.Name, property.GetValue(obj)?.ToString());
                }  
            }

            return dict;
        }
    }
}
