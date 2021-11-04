using System;

namespace OzonEdu.MerchendiseService.Domain.Exceptions.MerchendiseRequestAggregate
{
    public class MerchendiseRequestInvalidStatusException : ArgumentException
    {
        public MerchendiseRequestInvalidStatusException(string message) : base(message)
        {
        }
    }
}