using Newtonsoft.Json;
using Root.Code.Models.E01D.Net.Http.Json.Rpc;
using Root.Code.Shortcuts.E01D;

namespace Root.Code.Exts.E01D.Net.Http.Json.Rpc
{
    public static class RpcResponseExtensions
    {
        /// <summary>
        /// Parses and returns the result of the rpc response as the type specified. 
        /// Otherwise throws a parsing exception
        /// </summary>
        /// <typeparam name="T">Type of object to parse the response as</typeparam>
        /// <param name="response">Rpc response object</param>
        /// <param name="returnDefaultIfNull">Returns the type's default value if the result is null. Otherwise throws parsing exception</param>
        /// <returns>Result of response as type specified</returns>
        public static T GetResult<T>(this RpcResponse response, bool returnDefaultIfNull = true, JsonSerializerSettings settings = null)
        {
            return XJsonRpc.Api.Responses.GetResult<T>(response, returnDefaultIfNull, settings);
        }
    }
}
