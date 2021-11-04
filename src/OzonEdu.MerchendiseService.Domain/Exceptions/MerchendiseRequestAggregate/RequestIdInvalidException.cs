using System;

namespace OzonEdu.MerchendiseService.Domain.Exceptions.MerchendiseRequestAggregate
{
    public class RequestIdInvalidException : Exception
    {
        public RequestIdInvalidException(string message) : base(message)
        {
        }
    }
}