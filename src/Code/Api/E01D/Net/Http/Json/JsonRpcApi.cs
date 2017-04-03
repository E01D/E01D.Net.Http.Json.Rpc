using Root.Code.Api.E01D.Net.Http.Json.Rpc;

namespace Root.Code.Api.E01D.Net.Http.Json
{
    public class JsonRpcApi
    {
        

        public ClientApi Clients { get; set; } = new ClientApi();

        public ExceptionApi Exceptions { get; set; } = new ExceptionApi();

        public RequestApi Requests { get; set; } = new RequestApi();
        public ResponseApi Responses { get; set; } = new ResponseApi();
    }
}
