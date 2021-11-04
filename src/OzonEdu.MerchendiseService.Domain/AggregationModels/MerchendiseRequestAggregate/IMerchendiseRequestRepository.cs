using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchendiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchendiseService.Domain.Contracts;

namespace OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate
{
    public interface IMerchendiseRequestRepository : IRepository<MerchendiseRequest>
    {
        Task<MerchendiseRequest> FindByIdAsync(long id, CancellationToken cancellationToken = default);

        Task<MerchendiseRequest> FindByRequestIdAsync(MerchendiseRequestId merchendiseRequestId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<MerchendiseRequest>> FindAllByEmployeeIdAsync(EmployeeId employeeId,
            CancellationToken cancellationToken = default);

        Task DeleteAsync(MerchendiseRequest merchendiseRequest, CancellationToken cancellationToken = default);

        Task DeleteAllByEmployeeIdAsync(EmployeeId employeeId, CancellationToken cancellationToken = default);
    }
}