using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchendiseService.Domain.Contracts;
using OzonEdu.MerchendiseService.Domain.Exceptions;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Stubs
{
    public class MerchendiseRequestRepository : IMerchendiseRequestRepository
    {
        public IUnitOfWork UnitOfWork { get; } = new UnitOfWork();

        private Dictionary<long, MerchendiseRequest> _merchendiseRequests = new();

        public Task<MerchendiseRequest> CreateAsync(MerchendiseRequest itemToCreate,
            CancellationToken cancellationToken = default)
        {
            long newId = _merchendiseRequests.Count + 1;
            itemToCreate.SetRequestId(new MerchendiseRequestId(newId));
            _merchendiseRequests.Add(newId, itemToCreate);
            return Task.FromResult(itemToCreate);
        }

        public Task<MerchendiseRequest> UpdateAsync(MerchendiseRequest itemToUpdate,
            CancellationToken cancellationToken = default)
        {
            if (!_merchendiseRequests.ContainsKey(itemToUpdate.RequestId.Value))
                throw new NotFoundException($"Merchendise request with {itemToUpdate.RequestId} doesn't exist",
                    nameof(MerchendiseRequest));

            _merchendiseRequests[itemToUpdate.EmployeeId.Value] = itemToUpdate;
            return Task.FromResult(itemToUpdate);
        }

        public Task<MerchendiseRequest> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_merchendiseRequests.GetValueOrDefault(id, null));
        }

        public Task<MerchendiseRequest> FindByRequestIdAsync(MerchendiseRequestId merchendiseRequestId,
            CancellationToken cancellationToken = default)
        {
            return FindByIdAsync(merchendiseRequestId.Value, cancellationToken);
        }

        public async Task<IReadOnlyList<MerchendiseRequest>> FindAllByEmployeeIdAsync(EmployeeId employeeId,
            CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(_merchendiseRequests.Values
                .Where(request => request.EmployeeId == employeeId).ToList());
        }

        public Task DeleteAsync(MerchendiseRequest merchendiseRequest, CancellationToken cancellationToken = default)
        {
            if (!_merchendiseRequests.ContainsKey(merchendiseRequest.RequestId.Value))
                throw new NotFoundException($"Merchendise request with {merchendiseRequest.RequestId} doesn't exist",
                    nameof(MerchendiseRequest));

            _merchendiseRequests.Remove(merchendiseRequest.RequestId.Value);
            return Task.CompletedTask;
        }

        public Task DeleteAllByEmployeeIdAsync(EmployeeId employeeId, CancellationToken cancellationToken = default)
        {
            _merchendiseRequests = _merchendiseRequests
                .Where(item => item.Value.EmployeeId != employeeId)
                .ToDictionary(key => key.Key, value => value.Value);
            return Task.CompletedTask;
        }
    }
}