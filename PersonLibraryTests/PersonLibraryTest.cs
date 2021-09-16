using ClassLibrary1;
using Xunit;

namespace PersonLibraryTests
{
    public class PersonLibraryTest
    {
        [Fact]
        public void TestPersonCreation()
        {
            Person person = new Person("Adam", 25);
            bool isPersonCreated = person is not null;
            Assert.True(isPersonCreated);
        }

        [Fact]
        public void TestPersonPrinter()
        {
            Person person = new Person("Adam", 25);
            PersonPrinter personPrinter = new PersonPrinter(person);
            personPrinter.PrintPerson();
        }
    }
}
