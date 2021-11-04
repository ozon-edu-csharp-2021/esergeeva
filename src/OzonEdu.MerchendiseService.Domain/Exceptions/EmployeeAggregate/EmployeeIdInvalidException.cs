using System;

namespace OzonEdu.MerchendiseService.Domain.Exceptions.EmployeeAggregate
{
    public class EmployeeIdInvalidException : ArgumentException
    {
        public EmployeeIdInvalidException(string message) : base(message)
        {
        }
    }
}