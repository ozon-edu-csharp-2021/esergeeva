using System;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OzonEdu.MerchendiseService.DbInfrastructure.Extensions;
using OzonEdu.MerchendiseService.DomainInfrastructure.Extensions;
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

                services.AddDbInfrastructureServices();
                services.AddDomainInfrastructureServices();

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

        public static IHostBuilder ConfigurePorts(this IHostBuilder builder)
        {
            var httpPortEnv = Environment.GetEnvironmentVariable("HTTP_PORT");
            if (!int.TryParse(httpPortEnv, out var httpPort))
            {
                httpPort = 5000;
            }

            var grpcPortEnv = Environment.GetEnvironmentVariable("GRPC_PORT");
            if (!int.TryParse(grpcPortEnv, out var grpcPort))
            {
                grpcPort = 5002;
            }

            builder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(
                    options =>
                    {
                        Listen(options, httpPort, HttpProtocols.Http1);
                        Listen(options, grpcPort, HttpProtocols.Http2);
                    });
            });
            return builder;
        }

        static void Listen(KestrelServerOptions kestrelServerOptions, int? port, HttpProtocols protocols)
        {
            if (port != null)
            {
                kestrelServerOptions.Listen(IPAddress.Any, port.Value,
                    listenOptions => { listenOptions.Protocols = protocols; });
            }
        }
    }
}