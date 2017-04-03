using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Root.Code.Exceptions.E01D.Net.Http.Json.Rpc;
using Root.Code.Models.E01D.Net.Http.Json.Rpc;

namespace Root.Code.Api.E01D.Net.Http.Json.Rpc
{
    public class ResponseApi
    {
        public RpcResponse Create()
        {
            return new RpcResponse();
        }

        /// <param name="id">Request id</param>
        public RpcResponse Create(object id)
        {
            return new RpcResponse()
            {
                Id = id
            };
        }

        /// <param name="id">Request id</param>
        /// <param name="error">Request error</param>
        public RpcResponse Create(object id, RpcError error)
        {
            return new RpcResponse()
            {
                Id = id,
                Error = error
            };
        }

        /// <param name="id">Request id</param>
        /// <param name="result">Response result object</param>
        public RpcResponse Create(object id, JToken result)
        {
            return new RpcResponse()
            {
                Id = id,
                Result = result
            };
        }

        /// <summary>
        /// Parses and returns the result of the rpc response as the type specified. 
        /// Otherwise throws a parsing exception
        /// </summary>
        /// <typeparam name="T">Type of object to parse the response as</typeparam>
        /// <param name="response">Rpc response object</param>
        /// <param name="returnDefaultIfNull">Returns the type's default value if the result is null. Otherwise throws parsing exception</param>
        /// <returns>Result of response as type specified</returns>
        public T GetResult<T>(RpcResponse response, bool returnDefaultIfNull = true, JsonSerializerSettings settings = null)
        {
            if (response.Result == null)
            {
                if (!returnDefaultIfNull && default(T) != null)
                {
                    throw new RpcClientParseException($"Unable to convert the result (null) to type '{typeof(T)}'");
                }
                return default(T);
            }
            try
            {
                if (settings == null)
                {
                    return response.Result.ToObject<T>();
                }
                else
                {
                    JsonSerializer jsonSerializer = JsonSerializer.Create(settings);
                    return response.Result.ToObject<T>(jsonSerializer);
                }
            }
            catch (Exception ex)
            {
                throw new RpcClientParseException($"Unable to convert the result to type '{typeof(T)}'", ex);
            }
        }
    }
}
