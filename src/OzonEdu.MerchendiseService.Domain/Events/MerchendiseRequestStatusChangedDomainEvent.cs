using MediatR;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate;

namespace OzonEdu.MerchendiseService.Domain.Events
{
    public class MerchendiseRequestStatusChangedDomainEvent: INotification
    {
        public MerchendiseRequest MerchendiseRequest { get; }

        public MerchendiseRequestStatusChangedDomainEvent(MerchendiseRequest merchendiseRequest)
        {
            MerchendiseRequest = merchendiseRequest;
        }
    }
}