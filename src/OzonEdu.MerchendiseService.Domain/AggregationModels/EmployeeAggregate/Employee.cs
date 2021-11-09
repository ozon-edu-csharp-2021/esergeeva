using OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate.ValueObjects;
using OzonEdu.MerchendiseService.Domain.Exceptions.EmployeeAggregate;
using OzonEdu.MerchendiseService.Domain.Models;

namespace OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate
{
    public sealed class Employee: Entity
    {
        public EmployeeId EmployeeId { get; private set; }
        
        public HiringDate HiringDate { get; }
        
        public Employee(EmployeeId employeeId, HiringDate hiringDate)
        {
            SetEmployeeId(employeeId);
            HiringDate = hiringDate;
        }

        private void SetEmployeeId(EmployeeId employeeId)
        {
            if (employeeId.Value < 0)
                throw new EmployeeIdInvalidException("Employee id cannot be negative");

            EmployeeId = employeeId;
        }
    }
}