using System.Collections.Generic;
using OzonEdu.MerchendiseService.Domain.Models;

namespace OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseRequestAggregate.ValueObjects
{
    public sealed class MerchendiseRequestId: ValueObject
    {
        public long Value { get; }

        public MerchendiseRequestId(long value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return $"RequestId({Value})";
        }
    }
}