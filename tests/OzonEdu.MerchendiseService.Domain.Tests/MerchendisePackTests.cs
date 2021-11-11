using System.Collections.Generic;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate.ValueObjects;
using OzonEdu.MerchendiseService.Domain.Exceptions.MerchendisePackAggregate;
using Xunit;

namespace OzonEdu.MerchendiseService.Domain.Tests
{
    public class MerchendisePackTests
    {
        [Fact]
        public void CreateMerchendisePackSuccess()
        {
            var merchendisePackType = MerchendisePackType.WelcomePack;
            var items = new List<MerchendiseItem>
            {
                new MerchendiseItem(ItemType.Cup, new Sku(1), new Quantity(1)),
                new MerchendiseItem(ItemType.Socks, new Sku(1), new Quantity(2)),
            };

            var merchendisePack = new MerchendisePack(merchendisePackType, items);
            Assert.Equal(merchendisePackType, merchendisePack.PackType);
            Assert.Equal(items.Count, merchendisePack.Items.Count);
        }

        [Fact]
        public void CreateMerchendisePackWithEmptyItemsFail()
        {
            var merchendisePackType = MerchendisePackType.VeteranPack;
            var items = new List<MerchendiseItem>();

            Assert.Throws<MerchendisePackInvalidItemsException>(() => new MerchendisePack(merchendisePackType, items));
        }
        
        [Fact]
        public void CreateMerchendisePackWithNullItemsFail()
        {
            var merchendisePackType = MerchendisePackType.VeteranPack;

            Assert.Throws<MerchendisePackInvalidItemsException>(() => new MerchendisePack(merchendisePackType, null));
        }
        
        [Fact]
        public void CreateMerchendisePackWithNullPackTypeFail()
        {
            var items = new List<MerchendiseItem>
            {
                new MerchendiseItem(ItemType.Cup, new Sku(1), new Quantity(1)),
                new MerchendiseItem(ItemType.Socks, new Sku(1), new Quantity(2)),
            };

            Assert.Throws<MerchendisePackTypeInvalidException>(() => new MerchendisePack(null, items));
        }
    }
}