using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Root.Code.Constants.E01D.Net.Http.Json.Rpc;
using Root.Code.Exceptions.E01D.Net.Http.Json.Rpc;
using Root.Code.Framework.E01D;
using Root.Code.Models.E01D.Net.Http.Json.Rpc;

namespace Root.Code.Api.E01D.Net.Http.Json.Rpc
{
    public class ErrorApi
    {
        /// <param name="exception">Exception from Rpc request</param>
        /// <param name="showServerExceptions">
        /// Optional. If true the inner exceptions to errors (possibly from server code) will be shown. Defaults to false.
        /// </param>
        public RpcError Create(RpcException exception, bool showServerExceptions)
        {
            if (exception == null)
            {
                throw new ArgumentNullException(nameof(exception));
            }

            var error = new RpcError()
            {
                Code = (int) exception.ErrorCode,
                Message = X.GetExceptionMessage(exception, showServerExceptions),
                Data = exception.RpcData,
            };

            return error;

        }

        /// <param name="code">Rpc error code</param>
        /// <param name="message">Error message</param>
        /// <param name="data">Optional error data</param>
        public RpcError Create(RpcErrorCode code, string message, JToken data = null)
        {
            return Create((int) code, message, data);
        }

        /// <param name="code">Rpc error code</param>
        /// <param name="message">Error message</param>
        /// <param name="data">Optional error data</param>
        public RpcError Create(int code, string message, JToken data = null)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException(nameof(message));
            }

            var error = new RpcError()
            {
                Code = code,
                Message = message,
                Data = data,
            };

            return error;
        }
    }
}
