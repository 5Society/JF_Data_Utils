using JF.Utils.Domain.ValueObjects;

namespace JF.Utils.Domain.Test.ValueObjects
{
    public class SampleValueObject : ValueObject
    {
        public string? Value { get; }

        public SampleValueObject(string? value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value!;
        }

        internal bool NotEquals(SampleValueObject obj2)
        {
            return NotEqualOperator(this, obj2);
        }
    }
}
