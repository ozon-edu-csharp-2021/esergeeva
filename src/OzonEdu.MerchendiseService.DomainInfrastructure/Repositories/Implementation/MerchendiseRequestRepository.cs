using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate.ValueObjects;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate.ValueObjects;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Repositories.Implementation
{
    public class MerchendiseRequestRepository: IMerchendiseRequestRepository
    {
        public Task<MerchendiseRequest> CreateAsync(MerchendiseRequest itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchendiseRequest> UpdateAsync(MerchendiseRequest itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchendiseRequest> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchendiseRequest> FindByRequestIdAsync(MerchendiseRequestId merchendiseRequestId, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<MerchendiseRequest>> FindAllByEmployeeIdAsync(EmployeeId employeeId, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<MerchendiseRequest>> FindAllByStatus(MerchendiseRequestStatus status, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(MerchendiseRequest merchendiseRequest, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAllByEmployeeIdAsync(EmployeeId employeeId, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}