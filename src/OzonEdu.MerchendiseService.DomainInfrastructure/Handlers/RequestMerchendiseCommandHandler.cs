using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchendiseService.DomainInfrastructure.Commands.Models;
using OzonEdu.MerchendiseService.DomainInfrastructure.Commands.RequestMerchendise;
using OzonEdu.MerchendiseService.DomainInfrastructure.Extensions;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Handlers
{
    public class RequestMerchendiseCommandHandler :
        IRequestHandler<RequestMerchendiseCommand, RequestMerchendiseResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMerchendiseRequestRepository _merchendiseRequestRepository;
        private readonly IMerchendisePackRepository _merchendisePackRepository;

        public RequestMerchendiseCommandHandler(IEmployeeRepository employeeRepository,
            IMerchendiseRequestRepository merchendiseRequestRepository,
            IMerchendisePackRepository merchendisePackRepository)
        {
            _employeeRepository = employeeRepository;
            _merchendiseRequestRepository = merchendiseRequestRepository;
            _merchendisePackRepository = merchendisePackRepository;
        }

        public async Task<RequestMerchendiseResponse> Handle(RequestMerchendiseCommand request,
            CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.FindByIdAsync(request.EmployeeId, cancellationToken);
            if (employee is null)
                throw new Exception($"Employee with ${request.EmployeeId} not found");

            var employeeId = new EmployeeId(request.EmployeeId);
            MerchendisePackType requestedPackType = request.MerchendisePackType.ConvertToDomain();
            if (!await ValidateMerchendiseType(requestedPackType, employeeId, cancellationToken))
                throw new Exception($"Merchendise with type {request.MerchendisePackType} cannot be requested twice");

            var merchendisePack =
                await _merchendisePackRepository.FindByPackTypeAsync(requestedPackType, cancellationToken);
            if (merchendisePack is null)
                throw new Exception($"Merchendise pack hasn't been found for type ${requestedPackType}");

            // TODO Request merchendise pack from stock-api

            var result = await _merchendiseRequestRepository.CreateAsync(
                new MerchendiseRequest(employeeId, requestedPackType),
                cancellationToken);
            await _merchendiseRequestRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return new RequestMerchendiseResponse
            {
                MerchendiseRequestInfo = new MerchendiseRequestInfo
                {
                    RequestId = result.RequestId.Value,
                    MerchendisePackType = result.MerchendisePackType.Convert(),
                    RequestStatus = result.RequestStatus.Convert()
                }
            };
        }

        private async Task<bool> ValidateMerchendiseType(MerchendisePackType packType, EmployeeId employeeId,
            CancellationToken cancellationToken)
        {
            if (packType == MerchendisePackType.ConferenceListenerPack ||
                packType == MerchendisePackType.ConferenceSpeakerPack)
                return true;

            var employeeRequests =
                await _merchendiseRequestRepository.FindAllByEmployeeIdAsync(employeeId, cancellationToken);

            return employeeRequests.All(merchendiseRequest => merchendiseRequest.MerchendisePackType != packType);
        }
    }
}