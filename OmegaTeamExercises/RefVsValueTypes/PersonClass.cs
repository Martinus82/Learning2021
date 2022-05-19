using System;

namespace RefVsValueTypes
{
    public class PersonClass
    {
        // Reference types (class-es) are mutable:
        // At the core of classic object-oriented programming is the idea that an object has a strong identity
        // and encapsulates mutable state that evolves over time. C# has always worked great for that, But
        // sometimes you want pretty much the exact opposite, and here C#’s defaults have tended to get in the way, making things very laborious. => What does it mean? => override GetHashcode method from the object base class as well as Equals method.
        // (in new DotNet 5.0 we have "record" type which solves the issue of mutability of the reference types (classes). A "record" is still a class but it is not possible to change it. DotNet takes care about instance comparison...)
        // Equality comparison works based on comparing equality for every property

        public PersonClass(string firstName, string lastName, int age, Gender gender)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Age = age;
            Gender = gender;
        }

        public void UpdateAge(int newAge)
        {
            Age = newAge;
        }

        public string FirstName { get; }
        public string LastName { get; }
        public int Age { get; private set; }
        public Gender Gender { get; }

        public override string ToString()
        {
            return $"{FirstName} {LastName} {Age} {Gender}";
        }
    }
}
