using System;
using System.Threading;
using System.Threading.Tasks;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Repositories.Infrastructure.Interfaces
{
    public interface IDbConnectionFactory<TConnection> : IDisposable
    {
        Task<TConnection> CreateConnection(CancellationToken token);
    }
}