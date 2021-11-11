using OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate.ValueObjects;
using OzonEdu.MerchendiseService.Domain.Exceptions.EmployeeAggregate;
using OzonEdu.MerchendiseService.Domain.Models;

namespace OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate
{
    public sealed class Employee : Entity
    {
        public EmployeeId EmployeeId { get; private set; }

        public HiringDate HiringDate { get; private set; }

        public Employee(EmployeeId employeeId, HiringDate hiringDate)
        {
            SetEmployeeId(employeeId);
            SetHiringDate(hiringDate);
        }

        private void SetEmployeeId(EmployeeId employeeId)
        {
            if (employeeId is null)
                throw new EmployeeIdInvalidException("Employee id cannot be null");

            if (employeeId.Value < 0)
                throw new EmployeeIdInvalidException("Employee id cannot be negative");

            EmployeeId = employeeId;
        }

        private void SetHiringDate(HiringDate hiringDate)
        {
            HiringDate = hiringDate ?? throw new HiringDateInvalidException("Hiring date cannot be null");
        }
    }
}