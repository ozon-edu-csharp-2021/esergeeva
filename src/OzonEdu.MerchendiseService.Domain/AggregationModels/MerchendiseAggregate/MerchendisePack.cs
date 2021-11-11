using System.Collections.Generic;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate.ValueObjects;
using OzonEdu.MerchendiseService.Domain.Exceptions.MerchendisePackAggregate;
using OzonEdu.MerchendiseService.Domain.Models;

namespace OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate
{
    public sealed class MerchendisePack : Entity
    {
        public MerchendisePackType PackType { get; private set; }
        public IReadOnlyCollection<MerchendiseItem> Items { get; private set; }

        public MerchendisePack(MerchendisePackType packType, IReadOnlyCollection<MerchendiseItem> items)
        {
            SetPackType(packType);
            SetItems(items);
        }

        private void SetItems(IReadOnlyCollection<MerchendiseItem> items)
        {
            if (items is null || items.Count == 0)
                throw new MerchendisePackInvalidItemsException("Items list cannot be empty");

            Items = items;
        }

        private void SetPackType(MerchendisePackType packType)
        {
            PackType = packType ??
                       throw new MerchendisePackTypeInvalidException("Merchendise pack type cannot be null");
        }
    }
}