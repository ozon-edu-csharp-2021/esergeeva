using OzonEdu.MerchendiseService.Domain.Models;

namespace OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate.ValueObjects
{
    public sealed class MerchendiseRequestStatus: Enumeration
    {
        public static MerchendiseRequestStatus Unknown = new(1, nameof(InProgress));
        public static MerchendiseRequestStatus InProgress = new(2, nameof(InProgress));
        public static MerchendiseRequestStatus Queued = new(3, nameof(Queued));
        public static MerchendiseRequestStatus Done = new(4, nameof(Done));
        
        public MerchendiseRequestStatus(long id, string name) : base(id, name)
        {
        }
    }
}