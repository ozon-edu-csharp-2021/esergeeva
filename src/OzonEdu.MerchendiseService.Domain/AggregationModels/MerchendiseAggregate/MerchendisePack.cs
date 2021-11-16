using System.Collections.Generic;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate.ValueObjects;
using OzonEdu.MerchendiseService.Domain.Exceptions.MerchendisePackAggregate;
using OzonEdu.MerchendiseService.Domain.Models;

namespace OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate
{
    public sealed class MerchendisePack : Entity
    {
        public MerchendisePackType PackType { get; private set; }
        public IReadOnlyCollection<Sku> SkuItems { get; private set; }

        public MerchendisePack(MerchendisePackId packId, MerchendisePackType packType,
            IReadOnlyCollection<Sku> items)
        {
            SetPackId(packId);
            SetPackType(packType);
            SetItems(items);
        }

        private void SetPackId(MerchendisePackId packId)
        {
            if (packId is null)
                throw new MerchendisePackIdInvalidException("Merchendise pack id cannot be null");
            if (packId.Value <= 0)
                throw new MerchendisePackIdInvalidException("Merchendise pack id must be positive");
        }

        private void SetItems(IReadOnlyCollection<Sku> items)
        {
            if (items is null || items.Count == 0)
                throw new MerchendisePackInvalidItemsException("Items list cannot be empty");

            SkuItems = items;
        }

        private void SetPackType(MerchendisePackType packType)
        {
            PackType = packType ??
                       throw new MerchendisePackTypeInvalidException("Merchendise pack type cannot be null");
        }
    }
}