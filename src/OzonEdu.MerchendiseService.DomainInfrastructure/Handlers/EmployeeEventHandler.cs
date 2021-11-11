using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Enums;
using MediatR;
using OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate.ValueObjects;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate;
using OzonEdu.MerchendiseService.DomainInfrastructure.Commands.OuterCommands;
using OzonEdu.MerchendiseService.DomainInfrastructure.Commands.RequestMerchendise;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Handlers
{
    internal class EmployeeEventHandler : IRequestHandler<EmployeeEventCommand>
    {
        private readonly IMerchendiseRequestRepository _merchendiseRequestRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMediator _mediator;

        public EmployeeEventHandler(IMerchendiseRequestRepository merchendiseRequestRepository,
            IEmployeeRepository employeeRepository, IMediator mediator)
        {
            _merchendiseRequestRepository = merchendiseRequestRepository;
            _employeeRepository = employeeRepository;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(EmployeeEventCommand request, CancellationToken cancellationToken)
        {
            switch (request.EmployeeEventType)
            {
                case EmployeeEventType.Hiring:
                    await AddNewEmployee(request.EmployeeId, cancellationToken);
                    break;
                case EmployeeEventType.Dismissal:
                    await RemoveEmployee(request.EmployeeId, cancellationToken);
                    return new Unit();
                case EmployeeEventType.MerchDelivery:
                    return new Unit();
            }

            await RequestMerchendise(request, cancellationToken);
            return new Unit();
        }

        private async Task AddNewEmployee(long employeeId, CancellationToken cancellationToken)
        {
            var newEmployee = new Employee(new EmployeeId(employeeId), new HiringDate(DateTime.Now.ToUniversalTime()));
            await _employeeRepository.CreateAsync(newEmployee, cancellationToken);
        }

        private async Task RemoveEmployee(long employeeId, CancellationToken cancellationToken)
        {
            await _merchendiseRequestRepository.DeleteAllByEmployeeIdAsync(new EmployeeId(employeeId),
                cancellationToken);
            await _employeeRepository.DeleteByIdAsync(employeeId, cancellationToken);
        }

        private async Task RequestMerchendise(EmployeeEventCommand request, CancellationToken cancellationToken)
        {
            var requestMerchendiseCommand = new RequestMerchendiseCommand
            {
                EmployeeId = request.EmployeeId,
                MerchendisePackType = request.EmployeeEventType switch
                {
                    EmployeeEventType.Hiring => MerchType.WelcomePack,
                    EmployeeEventType.ProbationPeriodEnding => MerchType.ProbationPeriodEndingPack,
                    EmployeeEventType.ConferenceAttendance => MerchType.ConferenceListenerPack,
                    _ => throw new ArgumentOutOfRangeException(nameof(request.EmployeeEventType),
                        request.EmployeeEventType, "Unexpected employee event type")
                }
            };
            await _mediator.Send(requestMerchendiseCommand, cancellationToken);
        }
    }
}