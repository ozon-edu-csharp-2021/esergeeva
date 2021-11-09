using CSharpCourse.Core.Lib.Enums;
using MediatR;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Commands.KafkaEvents
{
    public sealed class EmployeeEventCommand: IRequest
    {
        public EmployeeEventType EmployeeEventType { get; init; }
        public long EmployeeId { get; init; }
    }
}