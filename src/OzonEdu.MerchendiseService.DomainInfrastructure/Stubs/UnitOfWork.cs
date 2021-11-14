using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchendiseService.Domain.Contracts;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Stubs
{
    internal class UnitOfWork : IUnitOfWork
    {
        public ValueTask StartTransaction(CancellationToken token)
        {
            return ValueTask.CompletedTask;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
        }
    }
}