using CSharpCourse.Core.Lib.Events;
using MediatR;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Commands.OuterCommands
{
    public sealed class SupplyShippedEventCommand: IRequest
    {
        private SupplyShippedEvent SupplyShippedEvent { get; init; }
    }
}