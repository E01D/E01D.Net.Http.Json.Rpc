using System.Collections.Generic;
using Root.Code.Constants.E01D.Net.Http.Json.Rpc;
using Root.Code.Models.E01D.Net.Http.Json.Rpc;

namespace Root.Code.Api.E01D.Net.Http.Json.Rpc
{
    public class RequestApi
    {
        /// <param name="id">Request id</param>
        /// <param name="method">Target method name</param>
        /// <param name="parameterList">List of parameters for the target method</param>
        public RpcRequest Create(object id, string method, object[] parameterList)
        {

            var request = new RpcRequest
            {
                Id = id,
                JsonRpcVersion = JsonRpcContants.JsonRpcVersion,
                Method = method,
                RawParameters = parameterList,
            };

            return request;

        }

        /// <param name="id">Request id</param>
        /// <param name="method">Target method name</param>
        /// <param name="parameterMap">Map of parameter name to parameter value for the target method</param>
        public RpcRequest Create(object id, string method, Dictionary<string, object> parameterMap)
        {
            var request = new RpcRequest
            {
                Id = id,
                JsonRpcVersion = JsonRpcContants.JsonRpcVersion,
                Method = method,
                RawParameters = parameterMap,
            };

            return request;
        }
    }
}
