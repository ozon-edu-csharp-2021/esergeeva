using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using OzonEdu.MerchendiseService.Infrastructure.Middlewares;

namespace OzonEdu.MerchendiseService.Infrastructure.StartupFilters
{
    public sealed class TerminalStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.Map("/version", builder => builder.UseMiddleware<VersionMiddleware>());
                app.Map("/live", builder => builder.UseMiddleware<AliveMiddleware>());
                app.Map("/ping", builder => builder.UseMiddleware<PingMiddleware>());
                app.UseMiddleware<LoggingMiddleware>();
                next(app);
            };
        }
    }
}