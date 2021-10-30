using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OzonEdu.MerchendiseService.Infrastructure.Middlewares
{
    public sealed class VersionMiddleware
    {
        public VersionMiddleware(RequestDelegate next)
        {
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "no version";
            await context.Response.WriteAsync(version);
        }
    }
}