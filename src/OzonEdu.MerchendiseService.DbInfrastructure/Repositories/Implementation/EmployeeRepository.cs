using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.MerchendiseService.DbInfrastructure.Infrastructure.Interfaces;
using OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate.ValueObjects;
using OzonEdu.MerchendiseService.Domain.Exceptions;

namespace OzonEdu.MerchendiseService.DbInfrastructure.Repositories.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int TimeoutSec = 5;

        public EmployeeRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }

        public async Task<Employee> CreateAsync(Employee itemToCreate, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                INSERT INTO employees (id, hiring_date) 
                VALUES (@EmployeeId, @HiringDate);
            ";

            var parameters = new
            {
                EmployeeId = itemToCreate.EmployeeId.Value,
                HiringDate = itemToCreate.HiringDate.Value
            };
            var commandDefinition = new CommandDefinition(sql, parameters,
                commandTimeout: TimeoutSec, cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);
            _changeTracker.Track(itemToCreate);
            return itemToCreate;
        }

        public async Task<Employee> UpdateAsync(Employee itemToUpdate, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                UPDATE employees
                SET hiring_date = @HiringDate
                WHERE id = @EmployeeId 
            ";

            var parameters = new
            {
                EmployeeId = itemToUpdate.EmployeeId.Value,
                HiringDate = itemToUpdate.HiringDate.Value
            };
            var commandDefinition = new CommandDefinition(sql, parameters,
                commandTimeout: TimeoutSec, cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);
            _changeTracker.Track(itemToUpdate);
            return itemToUpdate;
        }

        public async Task<Employee> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                SELECT employees.id, employees.hiring_date
                FROM employees
                WHERE employees.id = @EmployeeId;
            ";

            var parameters = new
            {
                EmployeeId = id
            };
            var commandDefinition = new CommandDefinition(sql, parameters,
                commandTimeout: TimeoutSec, cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var employee = await connection.QueryFirstOrDefaultAsync<Models.Employee>(commandDefinition);
            if (employee is null)
                return null;
            var result = new Employee(new EmployeeId(employee.Id), new HiringDate(employee.HiringDate));
            _changeTracker.Track(result);
            return result;
        }

        public async Task<Employee> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            var employee = await FindByIdAsync(id, cancellationToken);
            if (employee is null)
                throw new NotFoundException($"Employee with id {id} not found", nameof(Employee));
            return employee;
        }

        public Task<Employee> FindByEmployeeIdAsync(EmployeeId id, CancellationToken cancellationToken = default)
        {
            return FindByIdAsync(id.Value, cancellationToken);
        }

        public async Task DeleteByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            const string sql = @"
                DELETE FROM employees
                WHERE id = @EmployeeId
            ";
            var parameters = new
            {
                EmployeeId = id
            };

            var commandDefinition = new CommandDefinition(sql, parameters,
                commandTimeout: TimeoutSec, cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);
        }

        public Task DeleteByIdAsync(EmployeeId id, CancellationToken cancellationToken = default)
        {
            return DeleteByIdAsync(id.Value, cancellationToken);
        }
    }
}