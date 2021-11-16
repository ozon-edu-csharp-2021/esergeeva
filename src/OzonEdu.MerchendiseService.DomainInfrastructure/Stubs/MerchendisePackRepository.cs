using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate.ValueObjects;
using OzonEdu.MerchendiseService.Domain.Exceptions;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Stubs
{
    internal class MerchendisePackRepository : IMerchendisePackRepository
    {
        private static readonly Dictionary<MerchendisePackType, MerchendisePack> MerchendisePacks = new()
        {
            {
                MerchendisePackType.WelcomePack,
                new MerchendisePack(new MerchendisePackId(MerchendisePackType.WelcomePack.Id),
                    MerchendisePackType.WelcomePack, new[]
                    {
                        new Sku(1), new Sku(2)
                    })
            },
            {
                MerchendisePackType.ProbationPeriodEndingPack,
                new MerchendisePack(new MerchendisePackId(MerchendisePackType.ProbationPeriodEndingPack.Id),
                    MerchendisePackType.ProbationPeriodEndingPack, new[]
                    {
                        new Sku(3), new Sku(4)
                    })
            },
            {
                MerchendisePackType.ConferenceListenerPack,
                new MerchendisePack(new MerchendisePackId(MerchendisePackType.ConferenceListenerPack.Id),
                    MerchendisePackType.ConferenceListenerPack, new[]
                    {
                        new Sku(1), new Sku(2),
                        new Sku(3), new Sku(4)
                    })
            },
            {
                MerchendisePackType.ConferenceSpeakerPack,
                new MerchendisePack(new MerchendisePackId(MerchendisePackType.ConferenceListenerPack.Id),
                    MerchendisePackType.ConferenceSpeakerPack, new[]
                    {
                        new Sku(1), new Sku(2),
                        new Sku(5), new Sku(6)
                    })
            },
            {
                MerchendisePackType.VeteranPack, new MerchendisePack(
                    new MerchendisePackId(MerchendisePackType.VeteranPack.Id),
                    MerchendisePackType.VeteranPack, new[]
                    {
                        new Sku(1), new Sku(2),
                        new Sku(3), new Sku(4),
                        new Sku(5), new Sku(6)
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

        public Task<MerchendisePack> GetFirstByPackTypeAsync(MerchendisePackType packType,
            CancellationToken cancellationToken = default)
        {
            if (!MerchendisePacks.ContainsKey(packType))
                throw new NotFoundException($"Merchendise pack hasn't been found for type {packType}",
                    nameof(MerchendisePackType));
            return Task.FromResult(MerchendisePacks[packType]);
        }

        public Task<MerchendisePack> FindFirstByPackTypeAsync(MerchendisePackType packType,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult(MerchendisePacks.GetValueOrDefault(packType, null));
        }
    }
}