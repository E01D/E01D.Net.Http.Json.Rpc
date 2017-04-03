using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Root.Code.Constants.E01D.Net.Http.Json.Rpc;
using Root.Code.Exceptions.E01D.Net.Http.Json.Rpc;
using Root.Code.Models.E01D.Net.Http.Json.Rpc;

namespace Root.Code.Api.E01D.Net.Http.Json.Rpc
{
    public class ExceptionApi
    {
        public RpcException CreateException(RpcError error)
        {
            RpcException exception;
            switch ((RpcErrorCode)error.Code)
            {
                case RpcErrorCode.ParseError:
                    exception = new RpcParseException(error);
                    break;
                case RpcErrorCode.InvalidRequest:
                    exception = new RpcInvalidRequestException(error);
                    break;
                case RpcErrorCode.MethodNotFound:
                    exception = new RpcMethodNotFoundException(error);
                    break;
                case RpcErrorCode.InvalidParams:
                    exception = new RpcInvalidParametersException(error);
                    break;
                case RpcErrorCode.InternalError:
                    exception = new RpcInvalidParametersException(error);
                    break;
                default:
                    exception = new RpcCustomException(error);
                    break;
            }
            return exception;
        }
    }
}
