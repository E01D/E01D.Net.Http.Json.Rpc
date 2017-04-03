using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Root.Code.Constants.E01D.Net.Http.Json.Rpc;
using Root.Code.Exceptions.E01D.Net.Http.Json.Rpc;

namespace Root.Code.Models.E01D.Net.Http.Json.Rpc
{
    // <summary>
    /// Model to represent an Rpc response error
    /// </summary>
    [JsonObject]
    public class RpcError
    {
        [JsonConstructor]
        public RpcError()
        {
        }

       

        /// <summary>
        /// Rpc error code (Required)
        /// </summary>
        [JsonProperty("code", Required = Required.Always)]
        public int Code { get; set; }

        /// <summary>
        /// Error message (Required)
        /// </summary>
        [JsonProperty("message", Required = Required.Always)]
        public string Message { get; set; }

        /// <summary>
        /// Error data (Optional)
        /// </summary>
        [JsonProperty("data")]
        public JToken Data { get; set; }

      

       
    }
}
