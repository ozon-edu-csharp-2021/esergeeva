using CSharpCourse.Core.Lib.Events;
using MediatR;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Commands.KafkaEvents
{
    public class SupplyShippedEventCommand: IRequest
    {
        private SupplyShippedEvent SupplyShippedEvent { get; init; }
    }
}