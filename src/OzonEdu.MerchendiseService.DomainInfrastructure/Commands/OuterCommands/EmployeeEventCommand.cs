using CSharpCourse.Core.Lib.Enums;
using MediatR;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Commands.OuterCommands
{
    public sealed class EmployeeEventCommand: IRequest
    {
        public EmployeeEventType EmployeeEventType { get; init; }
        public long EmployeeId { get; init; }
    }
}