using System.Collections.Generic;

namespace CarSaloonEvidence.CarModel
{
    public class Manufacturer
    {
        public static ICollection<Manufacturer> GetManufacturers()
        {
            return new[]
            {
                new Manufacturer { Id = 1, Name = "Skoda" },
                new Manufacturer { Id = 2, Name = "Hyundai" },
                new Manufacturer { Id = 3, Name = "Kia" },
                new Manufacturer { Id = 4, Name = "Dodge" },
            };
        }

        public Manufacturer()
        {
        }

        public string Name { get; init; }
        public int Id { get; init; }
    }
}
