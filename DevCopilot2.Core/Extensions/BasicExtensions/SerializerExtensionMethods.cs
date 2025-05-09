using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DevCopilot2.Core.Extensions.BasicExtensions
{
    public static class SerializerExtensionMethods
    {
        public static T DeepCopy<T>(this T obj)
        {
            try
            {
                string json = JsonSerializer.Serialize(obj);
                return JsonSerializer.Deserialize<T>(json) ?? obj;
            }
            catch
            {
                return obj;
            }
        }
    }
}
