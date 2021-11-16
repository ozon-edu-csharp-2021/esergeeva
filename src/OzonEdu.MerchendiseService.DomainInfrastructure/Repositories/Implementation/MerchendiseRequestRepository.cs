using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate.ValueObjects;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate.ValueObjects;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate.ValueObjects;
using OzonEdu.MerchendiseService.DomainInfrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Repositories.Implementation
{
    public class MerchendiseRequestRepository : IMerchendiseRequestRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int TimeoutSec = 5;

        public MerchendiseRequestRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }

        public async Task<MerchendiseRequest> CreateAsync(MerchendiseRequest itemToCreate,
            CancellationToken cancellationToken = default)
        {
            const string sql = @"
                INSERT INTO merchendise_requests (employee_id, merchendise_pack_type, status) 
                VALUES (@EmployeeId, @PackType, @RequestStatus)
                RETURNING id;
            ";

            var parameters = new
            {
                EmployeeId = itemToCreate.EmployeeId.Value,
                PackType = itemToCreate.MerchendisePackType.Id,
                RequestStatus = itemToCreate.RequestStatus.Id
            };
            var commandDefinition = new CommandDefinition(sql, parameters,
                commandTimeout: TimeoutSec, cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var newId = await connection.ExecuteScalarAsync<long>(commandDefinition);
            itemToCreate.SetRequestId(new MerchendiseRequestId(newId));
            _changeTracker.Track(itemToCreate);
            return itemToCreate;
        }

        public async Task<MerchendiseRequest> UpdateAsync(MerchendiseRequest itemToUpdate,
            CancellationToken cancellationToken = default)
        {
            const string sql = @"
                UPDATE merchendise_requests 
                SET status = @RequestStatus, employee_id = @EmployeeId, merchendise_pack_type = @PackType
                WHERE id = @RequestId;
            ";

            var parameters = new
            {
                RequestId = itemToUpdate.RequestId.Value,
                EmployeeId = itemToUpdate.EmployeeId.Value,
                PackType = itemToUpdate.MerchendisePackType.Id,
                RequestStatus = itemToUpdate.RequestStatus.Id
            };
            var commandDefinition = new CommandDefinition(sql, parameters,
                commandTimeout: TimeoutSec, cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);
            _changeTracker.Track(itemToUpdate);
            return itemToUpdate;
        }

        public async Task<MerchendiseRequest> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT merchendise_requests.id, merchendise_requests.employee_id, 
                       merchendise_request_statuses.id, merchendise_request_statuses.name,
                       merchendise_pack_types.id, merchendise_pack_types.name
                FROM merchendise_requests
                INNER JOIN merchendise_pack_types on merchendise_requests.merchendise_pack_type = merchendise_pack_types.id
                INNER JOIN merchendise_request_statuses on merchendise_requests.status = merchendise_request_statuses.id
                WHERE merchendise_requests.id = @RequestId
            ";
            var parameters = new
            {
                RequestId = id
            };

            var commandDefinition = new CommandDefinition(sql, parameters,
                commandTimeout: TimeoutSec, cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var requests = await connection
                .QueryAsync<Models.MerchendiseRequest, Models.MerchendiseRequestStatus, Models.MerchendisePackType,
                    MerchendiseRequest>(commandDefinition,
                    (merchendiseRequest, packType, status) =>
                        new MerchendiseRequest(
                            new EmployeeId(merchendiseRequest.EmployeeId),
                            new MerchendisePackType(packType.Id, packType.Name),
                            new MerchendiseRequestStatus(status.Id, status.Name)
                        ));
            var result = requests.FirstOrDefault();
            if (result is not null)
                _changeTracker.Track(result);
            return result;
        }

        public Task<MerchendiseRequest> FindByRequestIdAsync(MerchendiseRequestId merchendiseRequestId,
            CancellationToken cancellationToken = default)
        {
            return FindByIdAsync(merchendiseRequestId.Value, cancellationToken);
        }

        public async Task<IReadOnlyList<MerchendiseRequest>> FindAllByEmployeeIdAsync(EmployeeId employeeId,
            CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT merchendise_requests.id, merchendise_requests.employee_id, 
                       merchendise_request_statuses.id, merchendise_request_statuses.name,
                       merchendise_pack_types.id, merchendise_pack_types.name
                FROM merchendise_requests
                INNER JOIN merchendise_pack_types on merchendise_requests.merchendise_pack_type = merchendise_pack_types.id
                INNER JOIN merchendise_request_statuses on merchendise_requests.status = merchendise_request_statuses.id
                WHERE merchendise_requests.employee_id = @EmployeeId
            ";
            var parameters = new
            {
                EmployeeId = employeeId.Value
            };

            var commandDefinition = new CommandDefinition(sql, parameters,
                commandTimeout: TimeoutSec, cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var requests = await connection
                .QueryAsync<Models.MerchendiseRequest, Models.MerchendiseRequestStatus, Models.MerchendisePackType,
                    MerchendiseRequest>(commandDefinition,
                    (merchendiseRequest, packType, status) =>
                        new MerchendiseRequest(
                            new EmployeeId(merchendiseRequest.EmployeeId),
                            new MerchendisePackType(packType.Id, packType.Name),
                            new MerchendiseRequestStatus(status.Id, status.Name)
                        ));
            var result = requests.ToList();
            foreach (var request in result)
                _changeTracker.Track(request);
            return result;
        }

        public async Task<List<MerchendiseRequest>> FindAllByStatus(MerchendiseRequestStatus requestStatus,
            CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT merchendise_requests.id, merchendise_requests.employee_id, 
                       merchendise_request_statuses.id, merchendise_request_statuses.name,
                       merchendise_pack_types.id, merchendise_pack_types.name
                FROM merchendise_requests
                INNER JOIN merchendise_pack_types on merchendise_requests.merchendise_pack_type = merchendise_pack_types.id
                INNER JOIN merchendise_request_statuses on merchendise_requests.status = merchendise_request_statuses.id
                WHERE merchendise_requests.status = @RequestStatus
            ";
            var parameters = new
            {
                RequestStatus = requestStatus.Id
            };

            var commandDefinition = new CommandDefinition(sql, parameters,
                commandTimeout: TimeoutSec, cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var requests = await connection
                .QueryAsync<Models.MerchendiseRequest, Models.MerchendiseRequestStatus, Models.MerchendisePackType,
                    MerchendiseRequest>(commandDefinition,
                    (merchendiseRequest, packType, status) =>
                        new MerchendiseRequest(
                            new EmployeeId(merchendiseRequest.EmployeeId),
                            new MerchendisePackType(packType.Id, packType.Name),
                            new MerchendiseRequestStatus(status.Id, status.Name)
                        ));
            var result = requests.ToList();
            foreach (var request in result)
                _changeTracker.Track(request);
            return result;
        }

        public async Task DeleteAsync(MerchendiseRequest merchendiseRequest, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                DELETE FROM merchendise_requests 
                WHERE id = @RequestId;
            ";

            var parameters = new
            {
                RequestId = merchendiseRequest.RequestId.Value,
            };
            var commandDefinition = new CommandDefinition(sql, parameters,
                commandTimeout: TimeoutSec, cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);
        }

        public async Task DeleteAllByEmployeeIdAsync(EmployeeId employeeId, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                DELETE FROM merchendise_requests 
                WHERE employee_id = @EmployeeId;
            ";

            var parameters = new
            {
                EmployeeId = employeeId.Value,
            };
            var commandDefinition = new CommandDefinition(sql, parameters,
                commandTimeout: TimeoutSec, cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);
        }
    }
}