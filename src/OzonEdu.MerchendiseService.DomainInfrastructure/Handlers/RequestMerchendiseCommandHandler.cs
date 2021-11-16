using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate.ValueObjects;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate.ValueObjects;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate;
using OzonEdu.MerchendiseService.Domain.Contracts;
using OzonEdu.MerchendiseService.Domain.Exceptions;
using OzonEdu.MerchendiseService.DomainInfrastructure.Commands.Models;
using OzonEdu.MerchendiseService.DomainInfrastructure.Commands.RequestMerchendise;
using OzonEdu.MerchendiseService.DomainInfrastructure.Extensions;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Handlers
{
    internal class RequestMerchendiseCommandHandler :
        IRequestHandler<RequestMerchendiseCommand, RequestMerchendiseResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMerchendiseRequestRepository _merchendiseRequestRepository;
        private readonly IMerchendisePackRepository _merchendisePackRepository;

        public RequestMerchendiseCommandHandler(IUnitOfWork unitOfWork, IEmployeeRepository employeeRepository,
            IMerchendiseRequestRepository merchendiseRequestRepository,
            IMerchendisePackRepository merchendisePackRepository)
        {
            _unitOfWork = unitOfWork;
            _employeeRepository = employeeRepository;
            _merchendiseRequestRepository = merchendiseRequestRepository;
            _merchendisePackRepository = merchendisePackRepository;
        }

        public async Task<RequestMerchendiseResponse> Handle(RequestMerchendiseCommand request,
            CancellationToken cancellationToken)
        {
            await _unitOfWork.StartTransaction(cancellationToken);
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId, cancellationToken);

            var employeeId = new EmployeeId(request.EmployeeId);
            MerchendisePackType requestedPackType = request.MerchendisePackType.ConvertToDomain();
            await ValidateMerchendiseType(requestedPackType, employeeId, cancellationToken);

            var merchendisePack =
                await _merchendisePackRepository.GetFirstByPackTypeAsync(requestedPackType, cancellationToken);

            // TODO Request merchendise pack from stock-api

            var result = await _merchendiseRequestRepository.CreateAsync(
                new MerchendiseRequest(employeeId, requestedPackType),
                cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
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

        private async Task ValidateMerchendiseType(MerchendisePackType packType, EmployeeId employeeId,
            CancellationToken cancellationToken)
        {
            if (packType == MerchendisePackType.ConferenceListenerPack ||
                packType == MerchendisePackType.ConferenceSpeakerPack)
                return;

            var employeeRequests =
                await _merchendiseRequestRepository.FindAllByEmployeeIdAsync(employeeId, cancellationToken);

            if (employeeRequests.Any(merchendiseRequest => merchendiseRequest.MerchendisePackType == packType))
                throw new ConflictException($"Merchendise with type {packType} cannot be requested twice",
                    nameof(MerchendisePackType));
        }
    }
}