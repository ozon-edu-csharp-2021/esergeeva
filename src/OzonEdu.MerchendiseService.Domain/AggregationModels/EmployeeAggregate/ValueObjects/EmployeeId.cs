using System.Collections.Generic;
using OzonEdu.MerchendiseService.Domain.Models;

namespace OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate.ValueObjects
{
    public sealed class EmployeeId: ValueObject
    {
        public long Value { get; }

        public EmployeeId(long value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return $"EmployeeId({Value})";
        }
    }
}