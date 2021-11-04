using System;

namespace OzonEdu.MerchendiseService.Domain.Exceptions.MerchendisePackAggregate
{
    public class MerchendisePackInvalidItemsException : ArgumentException
    {
        public MerchendisePackInvalidItemsException(string message) : base(message)
        {
        }
    }
}