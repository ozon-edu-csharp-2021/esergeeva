using System;

namespace OzonEdu.MerchendiseService.Domain.Exceptions.EmployeeAggregate
{
    public class HiringDateInvalidException : Exception
    {
        public HiringDateInvalidException(string message) : base(message)
        {
        }
    }
}