using System.Collections.Generic;

namespace OzonEdu.MerchendiseService.HttpModels
{
    public record GetAllMerchendiseResponse
    {
        public IReadOnlyList<MerchendiseRequestInfo> MerchendiseRequests { get; init; }
    }
}