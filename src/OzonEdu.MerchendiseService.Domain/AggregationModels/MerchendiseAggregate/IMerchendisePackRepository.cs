using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate.ValueObjects;
using OzonEdu.MerchendiseService.Domain.Contracts;

namespace OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate
{
    public interface IMerchendisePackRepository : IRepository<MerchendisePack>
    {
        Task<MerchendisePack> GetByPackTypeAsync(MerchendisePackType packType,
            CancellationToken cancellationToken = default);

        Task<MerchendisePack> FindByPackTypeAsync(MerchendisePackType packType,
            CancellationToken cancellationToken = default);
    }
}