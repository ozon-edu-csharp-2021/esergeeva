using Microsoft.Extensions.DependencyInjection;
using OzonEdu.MerchendiseService.DbInfrastructure.Repositories.Implementation;
using OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate;

namespace OzonEdu.MerchendiseService.DbInfrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDbInfrastructureServices(this IServiceCollection services)
        {
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IMerchendisePackRepository, MerchendisePackRepository>();
            services.AddScoped<IMerchendiseRequestRepository, MerchendiseRequestRepository>();

            return services;
        }
    }
}