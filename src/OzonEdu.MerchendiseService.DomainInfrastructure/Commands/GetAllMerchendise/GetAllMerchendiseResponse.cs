using System.Collections.Generic;
using OzonEdu.MerchendiseService.DomainInfrastructure.Commands.Models;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Commands.GetAllMerchendise
{
    public class GetAllMerchendiseResponse
    {
        public IReadOnlyList<MerchendiseRequestInfo> MerchendiseRequests { get; init; }
    }
}