using System.Threading.Tasks;
using Root.Code.Models.E01D.Net.Http.Json.Rpc;
using Root.Code.Shortcuts.E01D;

namespace Root.Code.Exts.E01D.Net.Http.Json.Rpc
{
    public static class ClientExts
    {
        public static async Task<RpcResponse> SendRequestAsync(this RpcClient client, RpcRequest request, string route = null)
        {
            return await XJsonRpc.Api.Clients.SendRequestAsync(client, request, route);
        }

        public static async Task<RpcResponse> SendRequestAsync(this RpcClient client, string method, string route = null, params object[] paramList)
        {
            return await XJsonRpc.Api.Clients.SendRequestAsync(client, method, route, paramList);
        }
    }
}
