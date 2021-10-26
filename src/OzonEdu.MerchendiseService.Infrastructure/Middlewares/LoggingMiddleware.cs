using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IO;
using OzonEdu.MerchendiseService.Infrastructure.Middlewares.Models;

namespace OzonEdu.MerchendiseService.Infrastructure.Middlewares
{
    public sealed class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager = new();
        private const int ReadChunkBufferLength = 4096;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public Task InvokeAsync(HttpContext context)
        {
            if (context.Request.ContentType == "application/grpc" ||
                context.Request.Path.StartsWithSegments("/swagger"))
            {
                return _next(context);
            }

            return ProcessHttp(context);
        }

        private async Task ProcessHttp(HttpContext context)
        {
            var request = await FormatRequest(context);

            var originalBody = context.Response.Body;
            await using MemoryStream newResponseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = newResponseBody;

            try
            {
                await _next(context);
            }
            finally
            {
                newResponseBody.Seek(0, SeekOrigin.Begin);
                await newResponseBody.CopyToAsync(originalBody);
                newResponseBody.Seek(0, SeekOrigin.Begin);

                var response = await FormatResponse(context, newResponseBody);

                WriteToLog(request, response);
            }
        }

        private void WriteToLog(LoggingRequest request, LoggingResponse response)
        {
            try
            {
                var requestResponseModel = new
                {
                    Request = request,
                    Response = response
                };
                _logger.LogInformation(JsonSerializer.Serialize(requestResponseModel));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Could not log request body");
            }
        }

        private async Task<LoggingRequest> FormatRequest(HttpContext context)
        {
            var request = context.Request;

            var loggingRequest = new LoggingRequest
            {
                Host = request.Host.ToString(),
                Path = request.Path.ToString(),
                QueryString = request.QueryString.ToString(),
                Headers = request.Headers.ToDictionary(k => k.Key, v => v.Value.ToString()),
                RequestBody = await GetRequestBody(request)
            };
            return loggingRequest;
        }

        private async Task<LoggingResponse> FormatResponse(HttpContext context, Stream body)
        {
            var response = context.Response;

            return new LoggingResponse
            {
                StatusCode = response.StatusCode,
                Headers = response.Headers.ToDictionary(k => k.Key, v => v.Value.ToString()),
                ResponseBody = await GetResponseBody(body)
            };
        }

        private async Task<object?> GetRequestBody(HttpRequest request)
        {
            request.EnableBuffering();
            try
            {
                await using var requestStream = _recyclableMemoryStreamManager.GetStream();
                await request.Body.CopyToAsync(requestStream);
                requestStream.Seek(0, SeekOrigin.Begin);
                var body = await ReadStreamInChunks(requestStream);
                return TryParseJson(body, out var jsonBody) ? jsonBody : body;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Couldn't read request body");
            }
            finally
            {
                request.Body.Seek(0, SeekOrigin.Begin);
            }

            return string.Empty;
        }

        private async Task<object?> GetResponseBody(Stream responseBody)
        {
            try
            {
                var body = await ReadStreamInChunks(responseBody);
                return TryParseJson(body, out var jsonBody) ? jsonBody : body;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Couldn't read response body");
            }

            return string.Empty;
        }

        private static bool TryParseJson(string input, out object? data)
        {
            try
            {
                data = JsonDocument.Parse(input);
                return true;
            }
            catch (Exception e)
            {
                data = null;
                return false;
            }
        }

        private static async Task<string> ReadStreamInChunks(Stream stream)
        {
            await using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[ReadChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = await reader.ReadBlockAsync(readChunk, 0, ReadChunkBufferLength);
                await textWriter.WriteAsync(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);

            return textWriter.ToString();
        }
    }
}