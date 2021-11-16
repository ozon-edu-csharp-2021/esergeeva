using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate.ValueObjects;
using OzonEdu.MerchendiseService.Domain.Exceptions;
using OzonEdu.MerchendiseService.Domain.Models;
using OzonEdu.MerchendiseService.DomainInfrastructure.Repositories.Infrastructure.Interfaces;
using DomainMerchendisePack = OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate.MerchendisePack;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Repositories.Implementation
{
    public class MerchendisePackRepository : IMerchendisePackRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int TimeoutSec = 5;

        public MerchendisePackRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }

        public async Task<DomainMerchendisePack> CreateAsync(DomainMerchendisePack itemToCreate,
            CancellationToken cancellationToken = default)
        {
            const string sql = @"
                INSERT INTO merchendise_packs (pack_type, sku_items) 
                VALUES (@PackType, @SkuItems);
            ";

            var parameters = new
            {
                PackType = itemToCreate.PackType.Id,
                SkuItems = itemToCreate.SkuItems
            };
            var commandDefinition = new CommandDefinition(sql, parameters,
                commandTimeout: TimeoutSec, cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);
            _changeTracker.Track(itemToCreate);
            return itemToCreate;
        }

        public async Task<DomainMerchendisePack> UpdateAsync(DomainMerchendisePack itemToUpdate,
            CancellationToken cancellationToken = default)
        {
            const string sql = @"
                UPDATE merchendise_packs
                SET pack_type = @PackType, sku_items = @SkuItems
                WHERE id = @PackId;
            ";

            var parameters = new
            {
                PackId = itemToUpdate.Id,
                PackType = itemToUpdate.PackType.Id,
                SkuItems = itemToUpdate.SkuItems
            };
            var commandDefinition = new CommandDefinition(sql, parameters,
                commandTimeout: TimeoutSec, cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);
            _changeTracker.Track(itemToUpdate);
            return itemToUpdate;
        }

        public async Task<DomainMerchendisePack> GetFirstByPackTypeAsync(MerchendisePackType packType,
            CancellationToken cancellationToken = default)
        {
            var merchendisePack = await FindFirstByPackTypeAsync(packType, cancellationToken);
            if (merchendisePack is null)
            {
                throw new NotFoundException($"Merchendise pack hasn't been found for type {packType}",
                    nameof(MerchendisePackType));
            }

            return merchendisePack;
        }

        public async Task<DomainMerchendisePack> FindFirstByPackTypeAsync(MerchendisePackType packType,
            CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT merchendise_packs.id, merchendise_packs.pack_type, merchendise_packs.sku_items,
                       merchendise_pack_types.id, merchendise_pack_types.name
                FROM merchendise_packs
                INNER JOIN merchendise_pack_types on merchendise_packs.pack_type = merchendise_pack_types.id
                WHERE merchendise_packs.pack_type = @pack_type;
            ";

            var parameters = new
            {
                pack_type = packType.Id,
            };
            var commandDefinition = new CommandDefinition(sql, parameters,
                commandTimeout: TimeoutSec, cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var merchendisePacks = await connection
                .QueryAsync<Models.MerchendisePack, Models.MerchendisePackType, int, DomainMerchendisePack>(
                    commandDefinition,
                    (merchendisePack, merchendisePackType, skuId) => new DomainMerchendisePack(
                        new MerchendisePackId(merchendisePackType.Id),
                        new MerchendisePackType(merchendisePackType.Id, merchendisePackType.Name),
                        merchendisePack.SkuItems.Select(id => new Sku(id)).ToList()
                        ));
            var merchendisePack = merchendisePacks.FirstOrDefault();
            if (merchendisePack != null)
                _changeTracker.Track(merchendisePack);
            return merchendisePack;
        }
    }
}