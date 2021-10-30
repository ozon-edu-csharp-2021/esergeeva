using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using OzonEdu.MerchendiseService.Presentation.GrpcServices;

namespace OzonEdu.MerchendiseService.Presentation
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
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