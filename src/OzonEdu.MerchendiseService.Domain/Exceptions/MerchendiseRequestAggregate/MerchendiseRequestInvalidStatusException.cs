using System;

namespace OzonEdu.MerchendiseService.Domain.Exceptions.MerchendiseRequestAggregate
{
    public class MerchendiseRequestInvalidStatusException : Exception
    {
        public MerchendiseRequestInvalidStatusException(string message) : base(message)
        {
        }
    }
}