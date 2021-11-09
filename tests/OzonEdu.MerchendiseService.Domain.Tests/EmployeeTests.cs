using System;
using OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate.ValueObjects;
using OzonEdu.MerchendiseService.Domain.Exceptions.EmployeeAggregate;
using Xunit;

namespace OzonEdu.MerchendiseService.Domain.Tests
{
    public class EmployeeTests
    {
        [Fact]
        public void CreateEmployeeSuccess()
        {
            var employeeId = 42;
            var hiringDate = new DateTime(2020, 02, 02);

            var employee = new Employee(new EmployeeId(employeeId), new HiringDate(hiringDate));

            Assert.Equal(employeeId, employee.EmployeeId.Value);
            Assert.Equal(hiringDate, employee.HiringDate.Value);
        }

        [Fact]
        public void CreateEmployeeWithNegativeIdFail()
        {
            var employeeId = -42;
            var hiringDate = new DateTime(2020, 02, 02);

            Assert.Throws<EmployeeIdInvalidException>(() =>
                new Employee(new EmployeeId(employeeId), new HiringDate(hiringDate)));
        }
    }
}