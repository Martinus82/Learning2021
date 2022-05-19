using System;

namespace RefVsValueTypes
{
    public struct PersonStruct
    {
        // Highly discourage to write struct for this purpose... it is just for demonstration!
        // Value types (struct-s) are immutable!
        // Equality comparison works based on comparing equality for every property
        // Structs should not contain huge logic!
        public PersonStruct(string firstName, string lastName, int age, Gender gender)
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
