using System.Collections.Generic;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Repositories.Models
{
    public class MerchendisePack
    {
        public long PackId { get; set; }
        
        public long PackType { get; set; }
        public ICollection<long> SkuItems { get; set; }
    }
}