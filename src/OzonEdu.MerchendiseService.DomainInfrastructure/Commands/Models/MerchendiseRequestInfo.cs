using CSharpCourse.Core.Lib.Enums;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Commands.Models
{
    public sealed class MerchendiseRequestInfo
    {
        public long RequestId { get; init; }
        public MerchType MerchendisePackType { get; init; }
        public RequestStatus RequestStatus { get; init; }
    }
}