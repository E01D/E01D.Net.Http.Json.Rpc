using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Root.Code.Exceptions.E01D.Net.Http.Json.Rpc;
using Root.Code.Models.E01D.Net.Http.Json.Rpc;
using Root.Code.Shortcuts.E01D;

namespace Root.Code.Exts.E01D.Net.Http.Json.Rpc
{
    public static class RpcErrorExts
    {
        public static RpcException CreateException(this RpcError error)
        {
            return XJsonRpc.Api.Exceptions.CreateException(error);
        }
    }
}
