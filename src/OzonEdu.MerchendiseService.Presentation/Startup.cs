using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using OzonEdu.MerchendiseService.Domain.Contracts;
using OzonEdu.MerchendiseService.DomainInfrastructure.Configuration;
using OzonEdu.MerchendiseService.DomainInfrastructure.Repositories.Infrastructure;
using OzonEdu.MerchendiseService.DomainInfrastructure.Repositories.Infrastructure.Interfaces;
using OzonEdu.MerchendiseService.Presentation.GrpcServices;

namespace OzonEdu.MerchendiseService.Presentation
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DatabaseConnectionOptions>(Configuration.GetSection(nameof(DatabaseConnectionOptions)));
            services.AddScoped<IDbConnectionFactory<NpgsqlConnection>, NpgsqlConnectionFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IChangeTracker, ChangeTracker>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<MerchendiseGrpcService>();
                endpoints.MapControllers();
            });
        }
    }
}