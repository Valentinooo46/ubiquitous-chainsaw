using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaPoshta
{
    public class NovaPoshtaResponse<T>
    {
        [JsonProperty("data")] public T[] data { get; set; }
    }
}
