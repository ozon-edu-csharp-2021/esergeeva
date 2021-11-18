using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OzonEdu.MerchendiseService.DomainInfrastructure.Commands.RequestMerchendise;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainInfrastructureServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(RequestMerchendiseCommand).Assembly);
            return services;
        }
    }
}