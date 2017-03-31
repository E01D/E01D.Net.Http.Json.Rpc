using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Root.Code.Components.E01D.Net.Http.Json.Rpc.JsonConvereters;
using Root.Code.Constants.E01D.Net.Http.Json.Rpc;
using Root.Code.Exts.E01D.Net.Http.Json.Rpc;

namespace Root.Code.Models.E01D.Net.Http.Json.Rpc
{
    [JsonObject]
    public class RpcResponse
    {
        [JsonConstructor]
        public RpcResponse()
        {
        }

        /// <summary>
        /// Request id (Required but nullable)
        /// </summary>
        [JsonProperty("id", Required = Required.AllowNull)]
        [JsonConverter(typeof(RpcIdJsonConverter))]
        public object Id { get; set; }

        /// <summary>
        /// Rpc request version (Required)
        /// </summary>
        [JsonProperty("jsonrpc", Required = Required.Always)]
        public string JsonRpcVersion { get; set; } = JsonRpcContants.JsonRpcVersion;

        /// <summary>
        /// Reponse result object (Required)
        /// </summary>
        [JsonProperty("result", Required = Required.Default)] //TODO somehow enforce this or an error, not both
        public JToken Result { get; set; }

        /// <summary>
        /// Error from processing Rpc request (Required)
        /// </summary>
        [JsonProperty("error", Required = Required.Default)]
        public RpcError Error { get; set; }

        [JsonIgnore]
        public bool HasError => this.Error != null;

        public void ThrowErrorIfExists()
        {
            if (this.HasError)
            {
                throw this.Error.CreateException();
            }
        }
    }
}
