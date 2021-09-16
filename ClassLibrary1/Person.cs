using System;

namespace ClassLibrary1
{
    public class Person : Object
    {
        public Person(string name, int age)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Age = age;
        }

        public string Name { get; }
        public int Age { get; }


        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
        }
    }
}
