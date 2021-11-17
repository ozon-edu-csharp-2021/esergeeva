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
            var packId = new MerchendisePackId(1);
            var merchendisePackType = MerchendisePackType.WelcomePack;
            var items = new List<Sku>
            {
                new Sku(1),
                new Sku(2)
            };

            var merchendisePack = new MerchendisePack(packId, merchendisePackType, items);
            Assert.Equal(merchendisePackType, merchendisePack.PackType);
            Assert.Equal(items.Count, merchendisePack.SkuItems.Count);
        }
        
        [Fact]
        public void CreateMerchendisePackWithNullIdFail()
        {
            var merchendisePackType = MerchendisePackType.VeteranPack;
            var items = new List<Sku>
            {
                new Sku(1),
                new Sku(2)
            };
            Assert.Throws<MerchendisePackIdInvalidException>(() => new MerchendisePack(null, merchendisePackType, items));
        }     

        [Fact]
        public void CreateMerchendisePackWithNegativeIdFail()
        {
            var packId = new MerchendisePackId(-1);
            var merchendisePackType = MerchendisePackType.VeteranPack;
            var items = new List<Sku>
            {
                new Sku(1),
                new Sku(2)
            };
            Assert.Throws<MerchendisePackIdInvalidException>(() => new MerchendisePack(packId, merchendisePackType, items));
        }

        [Fact]
        public void CreateMerchendisePackWithEmptyItemsFail()
        {
            var packId = new MerchendisePackId(1);
            var merchendisePackType = MerchendisePackType.VeteranPack;
            var items = new List<Sku>();

            Assert.Throws<MerchendisePackInvalidItemsException>(() => new MerchendisePack(packId, merchendisePackType, items));
        }
        
        [Fact]
        public void CreateMerchendisePackWithNullItemsFail()
        {
            var packId = new MerchendisePackId(1);
            var merchendisePackType = MerchendisePackType.VeteranPack;

            Assert.Throws<MerchendisePackInvalidItemsException>(() => new MerchendisePack(packId, merchendisePackType, null));
        }
        
        [Fact]
        public void CreateMerchendisePackWithNullPackTypeFail()
        {
            var packId = new MerchendisePackId(1);
            var items = new List<Sku>
            {
                new Sku(1),
                new Sku(2)
            };

            Assert.Throws<MerchendisePackTypeInvalidException>(() => new MerchendisePack(packId, null, items));
        }
    }
}