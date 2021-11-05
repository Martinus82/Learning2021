using System;

namespace ClassLibrary1
{
    public class Person : HumanBeing
    {
        public Person(string name, int age)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Age = age;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
        }

        public static long TickElapsedFrom(int year, Func<DateTime> nowFunction)
        {
            DateTime now = nowFunction();
            DateTime then = new DateTime(year, 1, 1);
            return (now - then).Ticks;
        }
    }
}
