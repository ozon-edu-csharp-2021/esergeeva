using OzonEdu.MerchendiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchendiseService.Domain.Events;
using OzonEdu.MerchendiseService.Domain.Exceptions.MerchendiseRequestAggregate;
using OzonEdu.MerchendiseService.Domain.Models;

namespace OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate
{
    public class MerchendiseRequest : Entity
    {
        public MerchendiseRequestId RequestId { get; private set; }
        public MerchendiseRequestStatus RequestStatus { get; private set; }
        public EmployeeId EmployeeId { get; }
        public MerchendisePackType MerchendisePackType { get; }

        public MerchendiseRequest(EmployeeId employeeId,
            MerchendisePackType merchendisePackType)
        {
            EmployeeId = employeeId;
            MerchendisePackType = merchendisePackType;
            ChangeStatus(MerchendiseRequestStatus.InProgress);
        }

        public void SetRequestId(MerchendiseRequestId requestId)
        {
            if (requestId.Value < 0)
                throw new RequestIdInvalidException("Merchendise request id cannot be negative");
            RequestId = requestId;
        }
        
        public void ChangeStatus(MerchendiseRequestStatus status)
        {
            if (RequestStatus == MerchendiseRequestStatus.Done)
                throw new MerchendiseRequestInvalidStatusException(
                    "Request already done. Status change is unavailable");

            if (RequestStatus != status)
            {
                RequestStatus = status;
                AddMerchendiseRequestStatusChanged();
            }
        }

        private void AddMerchendiseRequestStatusChanged()
        {
            AddDomainEvent(new MerchendiseRequestStatusChangedDomainEvent(this));
        }
    }
}