using System.Collections.Generic;
using OzonEdu.MerchendiseService.Domain.Models;

namespace OzonEdu.MerchendiseService.Domain.AggregationModels.MerchendiseAggregate.ValueObjects
{
    public sealed class MerchendisePackId: ValueObject
    {
        public long Value { get; }

        public MerchendisePackId(long value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return $"PackId({Value})";
        }
    }
}