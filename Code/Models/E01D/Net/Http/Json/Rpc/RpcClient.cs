using System;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Root.Code.Models.E01D.Net.Http.Json.Rpc
{
    public class RpcClient
    {
        /// <summary>
        /// Base url for the rpc server
        /// </summary>
        public Uri BaseUrl { get; set; }
        /// <summary>
        /// Authentication header value factory for the rpc request being sent. If the server requires
        /// authentication this requires a value. Otherwise it can be null
        /// </summary>
        public Func<Task<AuthenticationHeaderValue>> AuthHeaderValueFactory { get; set; }
        /// <summary>
        /// Json serialization settings that will be used in serialization and deserialization
        /// for rpc requests
        /// </summary>
        public JsonSerializerSettings JsonSerializerSettings { get; set; }
        /// <summary>
        /// Request encoding type for json content. If null, will default to UTF-8 
        /// </summary>
        public Encoding Encoding { get; set; }
        /// <summary>
        /// Request content type for json content. If null, will default to application/json
        /// </summary>
        public string ContentType { get; set; }

        
        

        


        
    }
}
