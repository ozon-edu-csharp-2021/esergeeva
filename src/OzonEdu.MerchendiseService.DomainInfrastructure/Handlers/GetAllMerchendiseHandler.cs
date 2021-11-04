using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate;
using OzonEdu.MerchendiseService.DomainInfrastructure.Commands.GetAllMerchendise;
using OzonEdu.MerchendiseService.DomainInfrastructure.Commands.Models;
using OzonEdu.MerchendiseService.DomainInfrastructure.Extensions;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Handlers
{
    public class GetAllMerchendiseHandler : IRequestHandler<GetAllMerchendiseCommand, GetAllMerchendiseResponse>
    {
        private readonly IMerchendiseRequestRepository _merchendiseRequestRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public GetAllMerchendiseHandler(IMerchendiseRequestRepository merchendiseRequestRepository,
            IEmployeeRepository employeeRepository)
        {
            _merchendiseRequestRepository = merchendiseRequestRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<GetAllMerchendiseResponse> Handle(GetAllMerchendiseCommand request,
            CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.FindByIdAsync(request.EmployeeId, cancellationToken);
            if (employee is null)
                throw new Exception($"Employee with ${request.EmployeeId} not found");

            var employeeRequests =
                await _merchendiseRequestRepository.FindAllByEmployeeIdAsync(employee.EmployeeId, cancellationToken);

            return new GetAllMerchendiseResponse
            {
                MerchendiseRequests = employeeRequests.Select(merchendiseRequest =>
                    new MerchendiseRequestInfo
                    {
                        RequestId = merchendiseRequest.RequestId.Value,
                        RequestStatus = merchendiseRequest.RequestStatus.Convert(),
                        MerchendisePackType = merchendiseRequest.MerchendisePackType.Convert()
                    }).ToList()
            };
        }
    }
}