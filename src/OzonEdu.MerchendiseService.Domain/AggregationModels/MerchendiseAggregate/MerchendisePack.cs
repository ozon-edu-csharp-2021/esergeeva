using System.Collections.Generic;
using OzonEdu.MerchendiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchendiseService.Domain.Exceptions.MerchendisePackAggregate;
using OzonEdu.MerchendiseService.Domain.Models;

namespace OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate
{
    public class MerchendisePack : Entity
    {
        public MerchendisePackType PackType { get; }
        public IReadOnlyCollection<MerchendiseItem> Items { get; private set; }

        public MerchendisePack(MerchendisePackType packType, IReadOnlyCollection<MerchendiseItem> items)
        {
            PackType = packType;
            SetItems(items);
        }

        private void SetItems(IReadOnlyCollection<MerchendiseItem> items)
        {
            if (items.Count == 0)
                throw new MerchendisePackInvalidItemsException("Items list shouldn't be empty");

            Items = items;
        }
    }
}