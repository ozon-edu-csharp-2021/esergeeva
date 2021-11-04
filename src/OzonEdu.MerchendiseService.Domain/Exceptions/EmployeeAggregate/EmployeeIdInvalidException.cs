using System;

namespace OzonEdu.MerchendiseService.Domain.Exceptions.EmployeeAggregate
{
    public class EmployeeIdInvalidException : Exception
    {
        public EmployeeIdInvalidException(string message) : base(message)
        {
        }
    }
}