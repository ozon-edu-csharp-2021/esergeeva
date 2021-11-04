using System.Collections.Generic;
using System.Linq;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchendiseService.Domain.Events;
using OzonEdu.MerchendiseService.Domain.Exceptions.MerchendiseRequestAggregate;
using Xunit;

namespace OzonEdu.MerchendiseService.Domain.Tests
{
    public class MerchendiseRequestTests
    {
        public static readonly IEnumerable<object[]> AllStatuses =
            new List<object[]>
            {
                new object[] {MerchendiseRequestStatus.InProgress},
                new object[] {MerchendiseRequestStatus.Queued},
                new object[] {MerchendiseRequestStatus.Done},
            };

        [Fact]
        public void CreateMerchendiseRequestSuccess()
        {
            var employeeId = 42;
            var merchendisePackType = MerchendisePackType.ConferenceListenerPack;

            var merchendiseRequest = new MerchendiseRequest(new EmployeeId(employeeId), merchendisePackType);

            Assert.Equal(employeeId, merchendiseRequest.EmployeeId.Value);
            Assert.Equal(merchendisePackType, merchendiseRequest.MerchendisePackType);
            Assert.Equal(MerchendiseRequestStatus.InProgress, merchendiseRequest.RequestStatus);
        }

        [Fact]
        public void SetRequestIdSuccess()
        {
            var requestId = 3;
            var merchendiseRequest = new MerchendiseRequest(new EmployeeId(42), MerchendisePackType.VeteranPack);

            merchendiseRequest.SetRequestId(new MerchendiseRequestId(requestId));

            Assert.Equal(requestId, merchendiseRequest.RequestId.Value);
        }

        [Fact]
        public void SetNegativeRequestIdFail()
        {
            var requestId = -3;
            var merchendiseRequest = new MerchendiseRequest(new EmployeeId(42), MerchendisePackType.VeteranPack);

            Assert.Throws<RequestIdInvalidException>(() =>
                merchendiseRequest.SetRequestId(new MerchendiseRequestId(requestId)));
        }

        [Theory]
        [MemberData(nameof(AllStatuses))]
        public void SetStatusWhileInProgressSuccess(MerchendiseRequestStatus status)
        {
            var merchendiseRequest = new MerchendiseRequest(new EmployeeId(42), MerchendisePackType.VeteranPack);
            merchendiseRequest.ChangeStatus(MerchendiseRequestStatus.InProgress);

            merchendiseRequest.ChangeStatus(status);
            Assert.Equal(status, merchendiseRequest.RequestStatus);
        }

        [Theory]
        [MemberData(nameof(AllStatuses))]
        public void SetStatusWhileQueuedSuccess(MerchendiseRequestStatus status)
        {
            var merchendiseRequest = new MerchendiseRequest(new EmployeeId(42), MerchendisePackType.VeteranPack);
            merchendiseRequest.ChangeStatus(MerchendiseRequestStatus.Queued);

            merchendiseRequest.ChangeStatus(status);
            Assert.Equal(status, merchendiseRequest.RequestStatus);
        }

        [Theory]
        [MemberData(nameof(AllStatuses))]
        public void SetStatusWhileDoneFail(MerchendiseRequestStatus status)
        {
            var merchendiseRequest = new MerchendiseRequest(new EmployeeId(42), MerchendisePackType.VeteranPack);
            merchendiseRequest.ChangeStatus(MerchendiseRequestStatus.Done);

            Assert.Throws<MerchendiseRequestInvalidStatusException>(() => merchendiseRequest.ChangeStatus(status));
        }

        [Fact]
        public void CreatingMerchendiseRequestInvokesStatusChangedDomainEvent()
        {
            var merchendiseRequest = new MerchendiseRequest(new EmployeeId(42), MerchendisePackType.VeteranPack);

            Assert.Equal(1, merchendiseRequest.DomainEvents.Count);
            var domainEvent = merchendiseRequest.DomainEvents.Last() as MerchendiseRequestStatusChangedDomainEvent;
            Assert.Equal(MerchendiseRequestStatus.InProgress, domainEvent!.MerchendiseRequest.RequestStatus);
        }

        [Fact]
        public void ChangingRequestStatusInvokesDomainEvent()
        {
            var merchendiseRequest = new MerchendiseRequest(new EmployeeId(42), MerchendisePackType.VeteranPack);
            var newStatus = MerchendiseRequestStatus.Queued;

            merchendiseRequest.ChangeStatus(newStatus);

            Assert.Equal(2, merchendiseRequest.DomainEvents.Count);
            var domainEvent = merchendiseRequest.DomainEvents.Last() as MerchendiseRequestStatusChangedDomainEvent;
            Assert.Equal(newStatus, domainEvent!.MerchendiseRequest.RequestStatus);
        }
        
        [Fact]
        public void SettingTheSameStatusDoesNotInvokeDomainEvent()
        {
            var merchendiseRequest = new MerchendiseRequest(new EmployeeId(42), MerchendisePackType.VeteranPack);
            var newStatus = MerchendiseRequestStatus.InProgress;

            merchendiseRequest.ChangeStatus(newStatus);

            Assert.Equal(1, merchendiseRequest.DomainEvents.Count);
        }
    }
}