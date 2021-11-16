using Microsoft.Extensions.DependencyInjection;
using MediatR;
using OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate;
using OzonEdu.MerchendiseService.DomainInfrastructure.Commands.RequestMerchendise;
using OzonEdu.MerchendiseService.DomainInfrastructure.Repositories.Implementation;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IMerchendisePackRepository, MerchendisePackRepository>();
            services.AddScoped<IMerchendiseRequestRepository, MerchendiseRequestRepository>();
            services.AddMediatR(typeof(RequestMerchendiseCommand).Assembly);

            return services;
        }
    }
}