using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OzonEdu.MerchendiseService.Infrastructure.Filters;
using OzonEdu.MerchendiseService.Infrastructure.Interceptors;
using OzonEdu.MerchendiseService.Infrastructure.StartupFilters;

namespace OzonEdu.MerchendiseService.Infrastructure.Extensions
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddInfrastructure(this IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddControllers(options => options.Filters.Add<ExceptionFilter>());

                services.AddSingleton<IStartupFilter, TerminalStartupFilter>();
                services.AddSingleton<IStartupFilter, SwaggerStartupFilter>();

                services.AddGrpc(options => options.Interceptors.Add<LoggingInterceptor>());

                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1",
                        new OpenApiInfo
                        {
                            Title = Assembly.GetEntryAssembly()?.GetName().Name,
                            Version = Assembly.GetEntryAssembly()?.GetName().Version?.ToString()
                        });
                });
            });

            return builder;
        }
    }
}