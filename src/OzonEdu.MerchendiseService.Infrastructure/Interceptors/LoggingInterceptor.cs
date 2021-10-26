using System;
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
        private static readonly JsonSerializerOptions JsonSerializerOptions = new() {WriteIndented = true};

        public LoggingInterceptor(ILogger<LoggingInterceptor> logger)
        {
            _logger = logger;
        }

        public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request,
            ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                var response = await base.UnaryServerHandler(request, context, continuation);
                WriteToLog(request, response);
                return response;
            }
            catch (Exception e)
            {
                WriteToLog(request, e.ToString());
                throw;
            }
        }

        private void WriteToLog<TRequest, TResponse>(TRequest request, TResponse response)
        {
            var requestResponseModel = new
            {
                Request = request,
                Response = response
            };
            _logger.LogInformation(JsonSerializer.Serialize(requestResponseModel, JsonSerializerOptions));
        }
    }
}