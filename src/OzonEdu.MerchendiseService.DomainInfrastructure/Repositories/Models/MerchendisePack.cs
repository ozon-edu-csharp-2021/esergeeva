using System.Collections.Generic;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Repositories.Models
{
    public class MerchendisePack
    {
        public long Id { get; set; }
        public int PackType { get; set; }
        public ICollection<MerchendiseItem> MerchendiseItems { get; set; }
    }
}