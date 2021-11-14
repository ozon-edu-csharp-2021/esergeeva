using System.Collections.Generic;
using OzonEdu.MerchendiseService.Domain.Models;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Repositories.Infrastructure.Interfaces
{
    public interface IChangeTracker
    {
        IEnumerable<Entity> TrackedEntities { get; }

        public void Track(Entity entity);
    }
}