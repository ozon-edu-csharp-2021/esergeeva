using System;
using System.Collections.Generic;
using OzonEdu.MerchendiseService.Domain.Models;

namespace OzonEdu.MerchendiseService.Domain.AggregationModels.EmployeeAggregate.ValueObjects
{
    public sealed class HiringDate: ValueObject
    {
        public DateTime Value { get; }

        public HiringDate(DateTime value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        
        public override string ToString()
        {
            return $"HiringDate({Value})";
        }
    }
}