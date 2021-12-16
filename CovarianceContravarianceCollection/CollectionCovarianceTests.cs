using System.Collections.Generic;

using FluentAssertions;

using Xunit;

namespace CovarianceContravarianceCollection
{
    public class CollectionCovarianceTests
    {
        [Fact]
        public void TestFruits()
        {
            IList<Fruit> fruits = new List<Fruit>
            {
                new Apple(),
                new Banana(),
                new Orange()
            };

            fruits.Should().ContainItemsAssignableTo<IEatable>();
        }
    }
}
