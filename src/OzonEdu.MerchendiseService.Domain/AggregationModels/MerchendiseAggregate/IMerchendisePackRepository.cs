using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate.ValueObjects;
using OzonEdu.MerchendiseService.Domain.Contracts;

namespace OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate
{
    public interface IMerchendisePackRepository : IRepository<MerchendisePack>
    {
        Task<MerchendisePack> GetFirstByPackTypeAsync(MerchendisePackType packType,
            CancellationToken cancellationToken = default);

        Task<MerchendisePack> FindFirstByPackTypeAsync(MerchendisePackType packType,
            CancellationToken cancellationToken = default);
    }
}