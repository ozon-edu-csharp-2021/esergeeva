using System;
using DomainRequestStatus = OzonEdu.MerchendiseService.DomainInfrastructure.Commands.Models.RequestStatus;
using GrpcRequestStatus = OzonEdu.MerchendiseService.Grpc.MerchendiseRequestInfo.Types.RequestStatus;
using HttpRequestStatus = OzonEdu.MerchendiseService.HttpModels.RequestStatus;

namespace OzonEdu.MerchendiseService.Presentation.Extensions
{
    public static class MerchendiseConverters
    {
        public static DomainRequestStatus Convert(this GrpcRequestStatus status)
        {
            return status switch
            {
                GrpcRequestStatus.InProgress => DomainRequestStatus.InProgress,
                GrpcRequestStatus.Queued => DomainRequestStatus.Queued,
                GrpcRequestStatus.Done => DomainRequestStatus.Done,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, "Unexpected request status")
            };
        }

        public static DomainRequestStatus Convert(this HttpRequestStatus status)
        {
            return status switch
            {
                HttpRequestStatus.InProgress => DomainRequestStatus.InProgress,
                HttpRequestStatus.Queued => DomainRequestStatus.Queued,
                HttpRequestStatus.Done => DomainRequestStatus.Done,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, "Unexpected request status")
            };
        }

        public static HttpRequestStatus ConvertToHttp(this DomainRequestStatus status)
        {
            return status switch
            {
                _ when status == DomainRequestStatus.InProgress => HttpRequestStatus.InProgress,
                _ when status == DomainRequestStatus.Queued => HttpRequestStatus.Queued,
                _ when status == DomainRequestStatus.Done => HttpRequestStatus.Done,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, "Unexpected request status")
            };
        }

        public static GrpcRequestStatus ConvertToGrpc(this DomainRequestStatus status)
        {
            return status switch
            {
                _ when status == DomainRequestStatus.InProgress => GrpcRequestStatus.InProgress,
                _ when status == DomainRequestStatus.Queued => GrpcRequestStatus.Queued,
                _ when status == DomainRequestStatus.Done => GrpcRequestStatus.Done,
                _ => throw new ArgumentOutOfRangeException(nameof(status), status, "Unexpected request status")
            };
        }
    }
}