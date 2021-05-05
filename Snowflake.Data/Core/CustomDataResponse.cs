using System.Collections.Generic;
using Newtonsoft.Json;

namespace Snowflake.Data.Core
{
    internal class CustomDataResponse<T> : BaseRestResponse
    {
        [JsonProperty(PropertyName = "data")]
        public T data { get; set; }
    }
}