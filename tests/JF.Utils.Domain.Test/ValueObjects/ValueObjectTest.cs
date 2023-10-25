using JF.Utils.Domain.ValueObjects;

namespace JF.Utils.Domain.Test.ValueObjects
{
    public class ValueObjectTest
    {

        [Fact]
        public void Equals_ReturnsTrue_WhenTwoValueObjectsAreEqual()
        {
            // Arrange
            var obj1 = new SampleValueObject("Test");
            var obj2 = new SampleValueObject("Test");

            // Act
            var result = obj1.Equals(obj2);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenTwoValueObjectsAreNotEqual()
        {
            // Arrange
            var obj1 = new SampleValueObject("Test1");
            var obj2 = new SampleValueObject("Test2");

            // Act
            var result = obj1.Equals(obj2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenTwoValueObjectsAreNotEqual2()
        {
            // Arrange
            var obj1 = new SampleValueObject("Test1");
            var obj2 = new SampleValueObject("Test2");

            // Act
            var result = obj1.NotEquals(obj2);

            // Assert
            Assert.True(result);
        }

        [Theory]
        [InlineData(null, "ValorNoNulo")]
        [InlineData("ValorNoNulo", null)]
        public void Equals_ReturnsFalse_WhenValueObjectsAreNull(string? str1, string? str2)
        {
            // Arrange
            var obj1 = new SampleValueObject(str1);
            var obj2 = new SampleValueObject(str2);

            // Act
            var result = obj1.Equals(obj2);

            // Assert
            Assert.False(result);
        }
        
        [Fact]
        public void Equals_ReturnsFalse_WhenObject2IsNull()
        {
            // Arrange
            var obj1 = new SampleValueObject("Prueba");
            object? obj2 = null;

            // Act
            var result = obj1.Equals(obj2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenTypeIsDifferent()
        {
            // Arrange
            var obj1 = new SampleValueObject("Prueba");
            var obj2 = "Prueba";

            // Act
            var result = obj1.Equals(obj2);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetHashCode_ReturnsSameValue_WhenTwoValueObjectsAreEqual()
        {
            // Arrange
            var obj1 = new SampleValueObject("Test");
            var obj2 = new SampleValueObject("Test");

            // Act
            var hashCode1 = obj1.GetHashCode();
            var hashCode2 = obj2.GetHashCode();

            // Assert
            Assert.Equal(hashCode1, hashCode2);
        }

        [Fact]
        public void GetHashCode_ReturnsDifferentValue_WhenTwoValueObjectsAreNotEqual()
        {
            // Arrange
            var obj1 = new SampleValueObject("Test1");
            var obj2 = new SampleValueObject("Test2");

            // Act
            var hashCode1 = obj1.GetHashCode();
            var hashCode2 = obj2.GetHashCode();

            // Assert
            Assert.NotEqual(hashCode1, hashCode2);
        }
    }
}
