using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.Extensions.Logging;

namespace OzonEdu.MerchendiseService.Infrastructure.Interceptors
{
    public class LoggingInterceptor : Interceptor
    {
        private readonly ILogger<LoggingInterceptor> _logger;

        public LoggingInterceptor(ILogger<LoggingInterceptor> logger)
        {
            _logger = logger;
        }

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request,
            ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            // TODO check exception
            var response = base.UnaryServerHandler(request, context, continuation);
            WriteToLog(request, response);
            return response;
        }

        public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(TRequest request, ClientInterceptorContext<TRequest, TResponse> context,
            AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
        {
            // TODO check exception
            var response = base.AsyncUnaryCall(request, context, continuation);
            WriteToLog(request, response);
            return response;
        }

        private void WriteToLog<TRequest, TResponse>(TRequest request, TResponse response)
        {
            var requestResponseModel = new
            {
                Request = request,
                Response = response
            };
            _logger.LogInformation(JsonSerializer.Serialize(requestResponseModel));
        }
    }
}