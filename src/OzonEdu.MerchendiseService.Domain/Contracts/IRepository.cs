using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchendiseService.Domain.Models;

namespace OzonEdu.MerchendiseService.Domain.Contracts
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> CreateAsync(TEntity itemToCreate, CancellationToken cancellationToken = default);

        Task<TEntity> UpdateAsync(TEntity itemToUpdate, CancellationToken cancellationToken = default);
    }
}