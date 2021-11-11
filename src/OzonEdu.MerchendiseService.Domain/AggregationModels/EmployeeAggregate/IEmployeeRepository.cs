using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate.ValueObjects;
using OzonEdu.MerchendiseService.Domain.Contracts;

namespace OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> FindByIdAsync(long id, CancellationToken cancellationToken = default);
        
        Task<Employee> GetByIdAsync(long id, CancellationToken cancellationToken = default);

        Task<Employee> FindByEmployeeIdAsync(EmployeeId id, CancellationToken cancellationToken = default);

        Task DeleteByIdAsync(long id, CancellationToken cancellationToken = default);
        
        Task DeleteByIdAsync(EmployeeId id, CancellationToken cancellationToken = default);
    }
}