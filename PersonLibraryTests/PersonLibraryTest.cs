using System;
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
            object manager = new Manager("Adam", 54);

            Type type = manager.GetType();
            Type managerType = typeof(Manager);

            Assert.True(type == managerType);
            Assert.True(manager is Manager);
            Assert.True(manager is HumanBeing);
            Assert.True(manager is Person);
            Assert.IsAssignableFrom<HumanBeing>(manager);
            Assert.IsAssignableFrom<Person>(manager);
            Assert.IsAssignableFrom<Manager>(manager);
            Assert.IsAssignableFrom<object>(manager);
            Assert.IsType<Manager>(manager);
            Assert.IsNotType<HumanBeing>(manager);
        }

        [Fact]
        public void TestManagerAsHuman()
        {
            Manager manager = new Manager("Adam", 54);
            Person person = manager;
            HumanBeing humanBeing = person;

            Person person2 = (Person)humanBeing;

            Assert.NotNull(person2);
        }

        [Fact]
        public void TestAsOperator()
        {
            Person person = new Person("Adam", 25);
            Manager manager = person as Manager;
            Assert.Null(manager);
        }

        [Fact]
        public void TestAsAndIsOperators()
        {
            // TODO: implement...
        }

        [Fact]
        public void TestPersonPrinter()
        {
            Person person = new Person("Adam", 25);
            PersonPrinter personPrinter = new PersonPrinter(person);
            personPrinter.PrintPerson();
        }

        [Fact]
        public void TestPersonCollection()
        {
            Person person1 = new Person("Adam", 25);
            Person person2 = new Person("Adam", 25);
            Person person3 = new Person("Adam", 25);
            Person person4 = new Person("Adam", 25);
            Person person5 = new Person("Adam", 25);

            List<Person> persons = new List<Person>
            {
                person1,
                person2,
                person3,
                person4,
                person5
            };
        }

        // Deterministic/honest API - Pure function example.
        [Fact]
        public void TestTicks()
        {
            DateTime Now() => new(2021, 1, 1);
            long expectedTicks = (Now() - new DateTime(2020, 1, 1)).Ticks;

            long elapsedTillNow = Person.TickElapsedFrom(2020, Now);
            Assert.Equal(elapsedTillNow, expectedTicks);
        }
    }
}
