using OzonEdu.MerchendiseService.Domain.Models;

namespace OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate
{
    public class ItemType : Enumeration
    {
        public static readonly ItemType TShirt = new(1, nameof(TShirt));
        public static readonly ItemType SweatShirt = new(2, nameof(SweatShirt));
        public static readonly ItemType Notepad = new(3, nameof(Notepad));
        public static readonly ItemType Socks = new(4, nameof(Socks));
        public static readonly ItemType Cup = new(5, nameof(Cup));
        public static readonly ItemType Pen = new(6, nameof(Pen));
        public static readonly ItemType Bag = new(6, nameof(Bag));

        public ItemType(int id, string name) : base(id, name)
        {
        }
    }
}