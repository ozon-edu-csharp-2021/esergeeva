using System.Collections.Generic;

namespace OzonEdu.MerchendiseService.Infrastructure.Middlewares.Models
{
    internal readonly struct LoggingRequest
    {
        public string Host { get; init; }
        public string Path { get; init; }
        public string QueryString { get; init; }
        public IReadOnlyDictionary<string, string> Headers { get; init; }
        public object? RequestBody { get; init; }
    }

    internal readonly struct LoggingResponse
    {
        public int StatusCode { get; init; }
        public IReadOnlyDictionary<string, string> Headers { get; init; }
        public object? ResponseBody { get; init; }
    }
}