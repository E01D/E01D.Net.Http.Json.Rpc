using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Root.Code.Api.E01D.Net.Http.Json;
using Root.Code.Models.E01D.Net.Http.Json.Rpc;

namespace Root.Code.Shortcuts.E01D
{
    public static class XJsonRpc
    {
        public static JsonRpcApi Api { get; set; } = new JsonRpcApi();

        public static RpcRequest CreateRequest(object id, string method, params object[] parameterList)
        {
            return Api.Requests.Create(id, method, parameterList);
            
        }

        /// <param name="id">Request id</param>
        /// <param name="method">Target method name</param>
        /// <param name="parameterMap">Map of parameter name to parameter value for the target method</param>
        public static RpcRequest CreateRequest(object id, string method, Dictionary<string, object> parameterMap)
        {
            return Api.Requests.Create(id, method, parameterMap);

            
        }


        public static RpcResponse CreateResponse()
        {
            return Api.Responses.Create();
        }

        /// <param name="id">Request id</param>
        public static RpcResponse CreateResponse(object id)
        {
            return Api.Responses.Create(id);
        }

        /// <param name="id">Request id</param>
        /// <param name="error">Request error</param>
        public static RpcResponse CreateResponse(object id, RpcError error)
        {
            return Api.Responses.Create(id, error);
        }

        /// <param name="id">Request id</param>
        /// <param name="result">Response result object</param>
        public static RpcResponse CreateResponse(object id, JToken result)
        {
            return Api.Responses.Create();
        }

        /// <param name="baseUrl">Base url for the rpc server</param>
        /// <param name="authHeaderValue">Http authentication header for rpc request</param>
        /// <param name="jsonSerializerSettings">Json serialization settings that will be used in serialization and deserialization for rpc requests</param>
        /// <param name="encoding">(Optional)Encoding type for request. Defaults to UTF-8</param>
        /// <param name="contentType">(Optional)Content type header for the request. Defaults to application/json</param>
        public static RpcClient CreateClient(Uri baseUrl, AuthenticationHeaderValue authHeaderValue = null, JsonSerializerSettings jsonSerializerSettings = null,
            Encoding encoding = null, string contentType = null)
        {
            var client = new RpcClient()
            {
                BaseUrl = baseUrl,
                AuthHeaderValueFactory = () => Task.FromResult(authHeaderValue),
                JsonSerializerSettings = jsonSerializerSettings,
                Encoding = encoding,
                ContentType = contentType,
            };

            return client;
        }

        /// <param name="baseUrl">Base url for the rpc server</param>
        /// <param name="authHeaderValueFactory">Http authentication header factory for rpc request</param>
        /// <param name="jsonSerializerSettings">Json serialization settings that will be used in serialization and deserialization for rpc requests</param>
        /// <param name="encoding">(Optional)Encoding type for request. Defaults to UTF-8</param>
        /// <param name="contentType">(Optional)Content type header for the request. Defaults to application/json</param>
        public static RpcClient CreateClient(Uri baseUrl, Func<Task<AuthenticationHeaderValue>> authHeaderValueFactory = null, JsonSerializerSettings jsonSerializerSettings = null,
            Encoding encoding = null, string contentType = null)
        {
            var client = new RpcClient()
            {
                BaseUrl = baseUrl,
                AuthHeaderValueFactory = authHeaderValueFactory,
                JsonSerializerSettings = jsonSerializerSettings,
                Encoding = encoding,
                ContentType = contentType,
            };

            return client;
        }
    }
}
