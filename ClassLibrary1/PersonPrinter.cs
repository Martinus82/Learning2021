using System;

namespace ClassLibrary1
{
    public class PersonPrinter
    {
        public Person Person { get; }

        public PersonPrinter(Person person)
        {
            Person = person ?? throw new ArgumentNullException(nameof(person));
        }

        public void PrintPerson()
        {
            Console.WriteLine(Person.ToString());
        }
    }
}