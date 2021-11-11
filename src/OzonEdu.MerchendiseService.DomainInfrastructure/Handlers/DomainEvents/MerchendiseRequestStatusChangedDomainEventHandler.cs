using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OzonEdu.MerchendiseService.Domain.Events;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Handlers.DomainEvents
{
    internal class MerchendiseRequestStatusChangedDomainEventHandler :
        INotificationHandler<MerchendiseRequestStatusChangedDomainEvent>
    {
        public Task Handle(MerchendiseRequestStatusChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            // TODO Send notification to employee 
            return Task.CompletedTask;
        }
    }
}