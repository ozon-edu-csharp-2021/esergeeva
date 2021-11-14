using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate.ValueObjects;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Repositories.Implementation
{
    public class MerchendisePackRepository: IMerchendisePackRepository
    {
        public Task<MerchendisePack> CreateAsync(MerchendisePack itemToCreate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchendisePack> UpdateAsync(MerchendisePack itemToUpdate, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchendisePack> GetByPackTypeAsync(MerchendisePackType packType, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<MerchendisePack> FindByPackTypeAsync(MerchendisePackType packType, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}