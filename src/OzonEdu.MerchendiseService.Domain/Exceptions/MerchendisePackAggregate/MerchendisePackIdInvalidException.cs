using System;

namespace OzonEdu.MerchendiseService.Domain.Exceptions.MerchendisePackAggregate
{
    public class MerchendisePackIdInvalidException : Exception
    {
        public MerchendisePackIdInvalidException(string message) : base(message)
        {
        }
    }
}