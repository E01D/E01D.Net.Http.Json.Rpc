using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Root.Code.Exceptions.E01D.Net.Http.Json.Rpc;
using Root.Code.Models.E01D.Net.Http.Json.Rpc;
using Root.Code.Shortcuts.E01D;

namespace Root.Code.Api.E01D.Net.Http.Json.Rpc
{
    public class ClientApi
    {
        /// <summary>
        /// Sends the specified rpc request to the server
        /// </summary>
        /// <param name="request">Single rpc request that will goto the rpc server</param>
        /// <param name="route">(Optional) Route that will append to the base url if the request method call is not located at the base route</param>
        /// <returns>The rpc response for the sent request</returns>
        public async Task<RpcResponse> SendRequestAsync(RpcClient client, RpcRequest request, string route = null)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            return await this.SendAsync(client, request, this.DeserilizeSingleRequest, route).ConfigureAwait(false);
        }

        private RpcResponse DeserilizeSingleRequest(RpcClient client, string json)
        {
            JToken token = JToken.Parse(json);
            JsonSerializer serializer = JsonSerializer.Create(client.JsonSerializerSettings);
            switch (token.Type)
            {
                case JTokenType.Object:
                    RpcResponse response = token.ToObject<RpcResponse>(serializer);
                    return response;
                case JTokenType.Array:
                    return token.ToObject<List<RpcResponse>>(serializer).SingleOrDefault();
                default:
                    throw new Exception("Cannot parse rpc response from server.");
            }
        }

        /// <summary>
        /// Sends the specified rpc request to the server (Wrapper for other SendRequestAsync)
        /// </summary>
        /// <param name="method">Rpc method that is to be called</param>
        /// <param name="route">(Optional) Route that will append to the base url if the request method call is not located at the base route</param>
        /// <param name="paramList">List of parameters (in order) for the rpc method</param>
        /// <returns>The rpc response for the sent request</returns>
        public async Task<RpcResponse> SendRequestAsync(RpcClient client, string method, string route = null, params object[] paramList)
        {
            if (string.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentNullException(nameof(method));
            }
            RpcRequest request = XJsonRpc.CreateRequest(Guid.NewGuid().ToString(), method, paramList);
            return await this.SendRequestAsync(client, request, route).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends the specified rpc requests to the server
        /// </summary>
        /// <param name="requests">Multiple rpc requests that will goto the rpc server</param>
        /// <param name="route">(Optional) Route that will append to the base url if the requests method call is not located at the base route</param>
        /// <returns>The rpc responses for the sent requests</returns>
        public async Task<List<RpcResponse>> SendBulkRequestAsync(RpcClient client, IEnumerable<RpcRequest> requests, string route = null)
        {
            if (requests == null)
            {
                throw new ArgumentNullException(nameof(requests));
            }
            List<RpcRequest> requestList = requests.ToList();
            return await this.SendAsync(client, requestList, this.DeserilizeBulkRequests, route).ConfigureAwait(false);
        }

        private List<RpcResponse> DeserilizeBulkRequests(RpcClient client, string json)
        {
            JToken token = JToken.Parse(json);
            JsonSerializer serializer = JsonSerializer.Create(client.JsonSerializerSettings);
            switch (token.Type)
            {
                case JTokenType.Object:
                    RpcResponse response = token.ToObject<RpcResponse>(serializer);
                    return new List<RpcResponse> { response };
                case JTokenType.Array:
                    return token.ToObject<List<RpcResponse>>(serializer);
                default:
                    throw new Exception("Cannout deserilize rpc response from server.");
            }
        }

        /// <summary>
        /// Sends the a http request to the server, posting the request in json format
        /// </summary>
        /// <param name="request">Request object that will goto the rpc server</param>
        /// <param name="deserializer">Function to deserialize the json response</param>
        /// <param name="route">(Optional) Route that will append to the base url if the request method call is not located at the base route</param>
        /// <returns>The response for the sent request</returns>
        private async Task<TResponse> SendAsync<TRequest, TResponse>(RpcClient client, TRequest request, Func<RpcClient, string, TResponse> deserializer, string route = null)
        {
            try
            {
                using (HttpClient httpClient = await this.GetHttpClientAsync(client))
                {
                    httpClient.BaseAddress = client.BaseUrl;

                    string rpcRequestJson = JsonConvert.SerializeObject(request, client.JsonSerializerSettings);
                    HttpContent httpContent = new StringContent(rpcRequestJson, client.Encoding ?? Encoding.UTF8, client.ContentType ?? "application/json");
                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(route, httpContent).ConfigureAwait(false);
                    httpResponseMessage.EnsureSuccessStatusCode();

                    string responseJson = await httpResponseMessage.Content.ReadAsStringAsync();

                    try
                    {
                        return deserializer(client, responseJson);
                    }
                    catch (JsonSerializationException)
                    {
                        throw new RpcClientUnknownException($"Unable to parse response from the rpc server. Response Json: {responseJson}");
                    }
                }
            }
            catch (Exception ex) when (!(ex is RpcClientException) && !(ex is RpcException))
            {
                throw new RpcClientUnknownException("Error occurred when trying to send rpc requests(s)", ex);
            }
        }

        private async Task<HttpClient> GetHttpClientAsync(RpcClient client)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = await client.AuthHeaderValueFactory();
            return httpClient;
        }
    }
}
