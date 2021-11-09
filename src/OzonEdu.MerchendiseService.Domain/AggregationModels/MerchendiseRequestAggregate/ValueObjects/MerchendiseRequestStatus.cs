using OzonEdu.MerchendiseService.Domain.Models;

namespace OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate.ValueObjects
{
    public sealed class MerchendiseRequestStatus: Enumeration
    {
        public static MerchendiseRequestStatus InProgress = new(1, nameof(InProgress));
        public static MerchendiseRequestStatus Queued = new(2, nameof(Queued));
        public static MerchendiseRequestStatus Done = new(3, nameof(Done));
        
        public MerchendiseRequestStatus(int id, string name) : base(id, name)
        {
        }
    }
}