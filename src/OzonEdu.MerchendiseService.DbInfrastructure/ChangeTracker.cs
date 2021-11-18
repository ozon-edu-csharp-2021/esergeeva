using System.Collections.Concurrent;
using System.Collections.Generic;
using OzonEdu.MerchendiseService.DbInfrastructure.Infrastructure.Interfaces;
using OzonEdu.MerchendiseService.Domain.Models;

namespace OzonEdu.MerchendiseService.DbInfrastructure.Infrastructure
{
    public class ChangeTracker : IChangeTracker
    {
        public IEnumerable<Entity> TrackedEntities => _usedEntitiesBackingField.ToArray();

        private readonly ConcurrentBag<Entity> _usedEntitiesBackingField;

        public ChangeTracker()
        {
            _usedEntitiesBackingField = new ConcurrentBag<Entity>();
        }
        
        public void Track(Entity entity)
        {
            _usedEntitiesBackingField.Add(entity);
        }
    }
}