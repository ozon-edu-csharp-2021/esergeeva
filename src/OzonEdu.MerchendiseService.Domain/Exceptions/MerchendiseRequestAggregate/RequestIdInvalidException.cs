using System;

namespace OzonEdu.MerchendiseService.Domain.Exceptions.MerchendiseRequestAggregate
{
    public class RequestIdInvalidException : ArgumentException
    {
        public RequestIdInvalidException(string message) : base(message)
        {
        }
    }
}