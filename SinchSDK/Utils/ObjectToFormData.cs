using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sinch.Utils
{
    internal static class ObjectUtils
    {
        internal static Dictionary<string, string > ToDictionary(this object obj)
        {
            string jsonString = JsonSerializer.Serialize(obj, SinchClient.JsonSerializerOptions);
            return JsonSerializer.Deserialize<Dictionary<string, string>>(jsonString, SinchClient.JsonSerializerOptions);
        }
    }
}
