using System.Collections.Generic;
using Newtonsoft.Json;
using Root.Code.Components.E01D.Net.Http.Json.Rpc.JsonConvereters;

namespace Root.Code.Models.E01D.Net.Http.Json.Rpc
{
    /// <summary>
	/// Model representing a Rpc request
	/// </summary>
	[JsonObject]
    public class RpcRequest
    {
        [JsonConstructor]
        public RpcRequest()
        {

        }

        /// <summary>
        /// Request Id (Optional)
        /// </summary>
        [JsonProperty("id")]
        [JsonConverter(typeof(RpcIdJsonConverter))]
        public object Id { get; set; }
        /// <summary>
        /// Version of the JsonRpc to be used (Required)
        /// </summary>
        [JsonProperty("jsonrpc", Required = Required.Always)]
        public string JsonRpcVersion { get; set; }
        /// <summary>
        /// Name of the target method (Required)
        /// </summary>
        [JsonProperty("method", Required = Required.Always)]
        public string Method { get; set; }
        /// <summary>
        /// Parameters to invoke the method with (Optional)
        /// </summary>
        [JsonProperty("params")]
        [JsonConverter(typeof(RpcParametersJsonConverter))]
        public object RawParameters { get; set; }

        /// <summary>
        /// Gets the raw parameters as an object array
        /// </summary>
        [JsonIgnore]
        public object[] ParameterList => this.RawParameters as object[] ?? new object[0];

        /// <summary>
        /// Gets the raw parameters as a parameter map
        /// </summary>
        [JsonIgnore]
        public Dictionary<string, object> ParameterMap => this.RawParameters as Dictionary<string, object>;
    }
}
