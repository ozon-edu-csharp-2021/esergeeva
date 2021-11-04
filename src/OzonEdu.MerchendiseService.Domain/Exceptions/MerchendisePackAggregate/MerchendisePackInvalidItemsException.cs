using System;

namespace OzonEdu.MerchendiseService.Domain.Exceptions.MerchendisePackAggregate
{
    public class MerchendisePackInvalidItemsException : Exception
    {
        public MerchendisePackInvalidItemsException(string message) : base(message)
        {
        }
    }
}