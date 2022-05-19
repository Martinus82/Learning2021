using System;

namespace RefVsValueTypes
{
    public class Program
    {
        // More info about the structs vs classes here:
        // https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/choosing-between-class-and-struct
        // https://docs.microsoft.com/en-us/dotnet/api/system.object.gethashcode?view=net-5.0
        // Look into base object class methods (ToString(), GetHashcode(), Equals(), GetType()) => These methods are common for all objects because all objects inherits from the base class "object"
        // What do we need to do in order to make those instances equal? => Implement IEquatable<> interface or override Equal() and GetHashcode() methods.
        // What happens when we pass a type into a methods as a parameter? What and how is it copied? => Reference to the object is copied.
        // Is reference equality the same thing like instance equality? => If two references to an object are equal means they points to the same object. But we can have a two different references to the same object.
        // What is similar to a reference in lower level programming languages like C, C++? => Reference in C# is like pointer in C++. It points to the allocated memory... There could be multiple pointers pointing to the same allocated memory space (object (on heap memory))
        // What does method GetHashcode() do? => The method gets the number which defines object equality (usually used in sorting algorithms where we need to compare object)
        // What's the difference between object.ReferenceEquals(obj1, obj2) vs object.Equals(obj1, obj2) vs obj.Equals(obj1) => If references are equal means they "points" to the same objects. Second method evaluates object equality (by calling Equals() method on the object instance), Third the same as second but not static.
        // Equality operators ??? => always static. Allow me to write something like: obj1 == obj1 => returns true if equal, otherwise false.
        static void Main(string[] args)
        {
            var person1 = new PersonClass("John", "Doe", 38, Gender.Male);

            Console.WriteLine(person1 + " ...created!");

            UpdatePerson(person1);

            // What is the person after Update method called?
            Console.WriteLine(person1);
        }

        private static void UpdatePerson(PersonClass person)
        {
            person.UpdateAge(21);

            // Create a new instance of the PersonClass
            person = new PersonClass("Another", "Person", 18, Gender.Other);
        }
    }
}
