using System;

namespace OzonEdu.MerchendiseService.Domain.Exceptions.MerchendisePackAggregate
{
    public class MerchendisePackTypeInvalidException : Exception
    {
        public MerchendisePackTypeInvalidException(string message) : base(message)
        {
        }
    }
}