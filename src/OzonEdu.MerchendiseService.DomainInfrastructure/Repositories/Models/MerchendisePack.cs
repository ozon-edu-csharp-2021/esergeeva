using System.Collections.Generic;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Repositories.Models
{
    public class MerchendisePack
    {
        public int PackType { get; set; }
        public ICollection<int> SkuItems { get; set; }
    }
}