using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.ValueObjects;
using OzonEdu.MerchendiseService.Domain.Contracts;

namespace OzonEdu.MerchendiseService.DomainInfrastructure.Stubs
{
    public class EmployeeRepository: IEmployeeRepository
    {
        public IUnitOfWork UnitOfWork { get; } = new UnitOfWork();

        private readonly Dictionary<long, Employee> _employees = new()
        {
            {1, new Employee(new EmployeeId(1), new HiringDate(new DateTime(2021, 04, 01)))},
            {2, new Employee(new EmployeeId(2), new HiringDate(new DateTime(2020, 04, 01)))},
            {3, new Employee(new EmployeeId(3), new HiringDate(new DateTime(2019, 04, 01)))},
            {4, new Employee(new EmployeeId(4), new HiringDate(new DateTime(2018, 04, 01)))},
            {5, new Employee(new EmployeeId(5), new HiringDate(new DateTime(2017, 04, 01)))},
        };

        public Task<Employee> CreateAsync(Employee itemToCreate, CancellationToken cancellationToken = default)
        {
            if (_employees.ContainsKey(itemToCreate.EmployeeId.Value))
                throw new Exception($"Employee with ${itemToCreate.EmployeeId} already exists");
            
            _employees.Add(itemToCreate.EmployeeId.Value, itemToCreate);
            return Task.FromResult(itemToCreate);
        }

        public Task<Employee> UpdateAsync(Employee itemToUpdate, CancellationToken cancellationToken = default)
        {
            if (!_employees.ContainsKey(itemToUpdate.EmployeeId.Value))
                throw new Exception($"Employee with ${itemToUpdate.EmployeeId} doesn't exist");

            _employees[itemToUpdate.EmployeeId.Value] = itemToUpdate;
            return Task.FromResult(itemToUpdate);
        }

        public Task<Employee> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_employees.GetValueOrDefault(id, null));
        }

        public Task<Employee> FindByEmployeeIdAsync(EmployeeId id, CancellationToken cancellationToken = default)
        {
            return FindByIdAsync(id.Value, cancellationToken);
        }

        public Task DeleteByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            if (!_employees.ContainsKey(id))
                throw new Exception($"Employee with id ${id} doesn't exist");

            _employees.Remove(id);
            return Task.CompletedTask;
        }

        public Task DeleteByIdAsync(EmployeeId id, CancellationToken cancellationToken = default)
        {
            return DeleteByIdAsync(id.Value, cancellationToken);
        }
    }
}