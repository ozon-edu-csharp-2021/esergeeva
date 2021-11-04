using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchendiseService.Domain.Contracts;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Stubs
{
    public class MerchendisePackRepository : IMerchendisePackRepository
    {
        public IUnitOfWork UnitOfWork { get; } = new UnitOfWork();

        private static readonly Dictionary<ItemType, MerchendiseItem> Items = new()
        {
            {ItemType.Pen, new MerchendiseItem(ItemType.Pen, new Sku(1), new Quantity(3))},
            {ItemType.Notepad, new MerchendiseItem(ItemType.Notepad, new Sku(1), new Quantity(2))},
            {ItemType.Cup, new MerchendiseItem(ItemType.Cup, new Sku(1), new Quantity(1))},
            {ItemType.Socks, new MerchendiseItem(ItemType.Socks, new Sku(1), new Quantity(4))},
            {ItemType.Bag, new MerchendiseItem(ItemType.Bag, new Sku(1), new Quantity(1))},
            {ItemType.TShirt, new MerchendiseItem(ItemType.TShirt, new Sku(1), new Quantity(2))},
            {ItemType.SweatShirt, new MerchendiseItem(ItemType.SweatShirt, new Sku(1), new Quantity(1))},
        };

        private static readonly Dictionary<MerchendisePackType, MerchendisePack> MerchendisePacks = new()
        {
            {
                MerchendisePackType.WelcomePack,
                new MerchendisePack(MerchendisePackType.WelcomePack, new[]
                {
                    Items[ItemType.Pen], Items[ItemType.Notepad]
                })
            },
            {
                MerchendisePackType.ProbationPeriodEndingPack,
                new MerchendisePack(MerchendisePackType.WelcomePack, new[]
                {
                    Items[ItemType.Cup], Items[ItemType.Socks]
                })
            },
            {
                MerchendisePackType.ConferenceListenerPack,
                new MerchendisePack(MerchendisePackType.WelcomePack, new[]
                {
                    Items[ItemType.Pen], Items[ItemType.Notepad],
                    Items[ItemType.TShirt], Items[ItemType.Bag]
                })
            },
            {
                MerchendisePackType.ConferenceSpeakerPack,
                new MerchendisePack(MerchendisePackType.WelcomePack, new[]
                {
                    Items[ItemType.Pen], Items[ItemType.Notepad],
                    Items[ItemType.SweatShirt], Items[ItemType.Bag]
                })
            },
            {
                MerchendisePackType.VeteranPack, new MerchendisePack(MerchendisePackType.WelcomePack, new[]
                {
                    Items[ItemType.Pen], Items[ItemType.Notepad],
                    Items[ItemType.Cup], Items[ItemType.Socks],
                    Items[ItemType.TShirt], Items[ItemType.SweatShirt], Items[ItemType.Bag]
                })
            }
        };

        public Task<MerchendisePack> CreateAsync(MerchendisePack itemToCreate,
            CancellationToken cancellationToken = default)
        {
            throw new NotSupportedException();
        }

        public Task<MerchendisePack> UpdateAsync(MerchendisePack itemToUpdate,
            CancellationToken cancellationToken = default)
        {
            throw new NotSupportedException();
        }

        public Task<MerchendisePack> FindByPackTypeAsync(MerchendisePackType packType,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(MerchendisePacks.GetValueOrDefault(packType, null));
        }
    }
}