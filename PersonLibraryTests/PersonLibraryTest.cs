using System.Collections.Generic;

using ClassLibrary1;

using Xunit;

namespace PersonLibraryTests
{
    public class PersonLibraryTest
    {
        [Fact]
        public void TestHumanBeingCreation()
        {
            HumanBeing person = new HumanBeing()
            {
                Name = "Adam",
                Age = 25
            };

            bool isCreated = person is not null;
            Assert.True(isCreated);
        }

        [Fact]
        public void TestPersonCreation()
        {
            Person person = new Person("Adam", 25);

            bool isPersonCreated = person is not null;
            Assert.True(isPersonCreated);
        }

        [Fact]
        public void TestManagerCreation()
        {
            Person manager = new Manager("Adam", 25);
            HumanBeing humanBeing = manager;

            // TODO: Add asserts for human type.

            if (humanBeing is Manager)
            {

            }

            if (humanBeing is Person)
            {

            }

            if (humanBeing is HumanBeing)
            {

            }

            Manager mgr = (Manager)humanBeing;

            humanBeing.Sleep();
            humanBeing.Eat();
            //humanBeing.IncreaseSalary();
            //manager.IncreaseSalary();

            bool isCreated = humanBeing is not null;
            Assert.True(isCreated);
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
