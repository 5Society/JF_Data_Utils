using JF.Utils.Domain.ValueObjects;

namespace JF.Utils.Domain.Test.ValueObjects
{
    public class SampleValueObject : ValueObject
    {
        public string Value { get; }

        public SampleValueObject(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
