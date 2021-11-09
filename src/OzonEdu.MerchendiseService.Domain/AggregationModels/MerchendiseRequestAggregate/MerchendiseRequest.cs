using OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate.ValueObjects;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate.ValueObjects;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate.ValueObjects;
using OzonEdu.MerchendiseService.Domain.Events;
using OzonEdu.MerchendiseService.Domain.Exceptions.EmployeeAggregate;
using OzonEdu.MerchendiseService.Domain.Exceptions.MerchendisePackAggregate;
using OzonEdu.MerchendiseService.Domain.Exceptions.MerchendiseRequestAggregate;
using OzonEdu.MerchendiseService.Domain.Models;

namespace OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate
{
    public sealed class MerchendiseRequest : Entity
    {
        public MerchendiseRequestId RequestId { get; private set; }
        public MerchendiseRequestStatus RequestStatus { get; private set; }
        public EmployeeId EmployeeId { get; private set; }
        public MerchendisePackType MerchendisePackType { get; private set; }

        public MerchendiseRequest(EmployeeId employeeId,
            MerchendisePackType merchendisePackType)
        {
            SetEmployeeId(employeeId);
            SetPackType(merchendisePackType);
            ChangeStatus(MerchendiseRequestStatus.Unknown);
        }

        public void SetRequestId(MerchendiseRequestId requestId)
        {
            if (requestId is null)
                throw new RequestIdInvalidException("Merchendise request id cannot be null");
            if (requestId.Value < 0)
                throw new RequestIdInvalidException("Merchendise request id cannot be negative");
            RequestId = requestId;
        }

        public void ChangeStatus(MerchendiseRequestStatus status)
        {
            if (status is null)
                throw new MerchendiseRequestInvalidStatusException("Merchendise status cannot be null");

            if (RequestStatus == MerchendiseRequestStatus.Done)
                throw new MerchendiseRequestInvalidStatusException(
                    "Request already done. Status change is unavailable");

            if (RequestStatus != status)
            {
                RequestStatus = status;
                AddMerchendiseRequestStatusChanged();
            }
        }

        private void SetEmployeeId(EmployeeId employeeId)
        {
            if (employeeId is null)
                throw new EmployeeIdInvalidException("Employee id cannot be null");

            if (employeeId.Value < 0)
                throw new EmployeeIdInvalidException("Employee id cannot be negative");

            EmployeeId = employeeId;
        }

        private void SetPackType(MerchendisePackType packType)
        {
            MerchendisePackType = packType ??
                                  throw new MerchendisePackTypeInvalidException("Merchendise pack type cannot be null");
        }

        private void AddMerchendiseRequestStatusChanged()
        {
            AddDomainEvent(new MerchendiseRequestStatusChangedDomainEvent(this));
        }
    }
}