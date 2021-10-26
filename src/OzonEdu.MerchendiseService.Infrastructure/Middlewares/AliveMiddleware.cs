using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace OzonEdu.MerchendiseService.Infrastructure.Middlewares
{
    public sealed class AliveMiddleware
    {
        public AliveMiddleware(RequestDelegate next)
        {
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync("I am alive");
        }
    }
}