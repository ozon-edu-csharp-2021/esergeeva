using System.Collections.Generic;
using OzonEdu.MerchendiseService.Domain.Models;

namespace OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate
{
    public class MerchendiseItem: ValueObject
    {
        public ItemType ItemType { get; }
        public Sku Sku { get; }
        public Quantity Quantity { get; }

        public MerchendiseItem(ItemType itemType, Sku sku, Quantity quantity)
        {
            ItemType = itemType;
            Sku = sku;
            Quantity = quantity;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Sku;
            yield return Quantity;
            yield return ItemType;
        }
        
        public override string ToString()
        {
            return $"MerchendiseItem[${ItemType}, ${Sku}, ${Quantity}]";
        }
    }
}