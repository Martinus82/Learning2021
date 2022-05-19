using System;

namespace CarSaloonEvidence.CarModel
{
    public class Car
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string ModelName { get; set; }
        public string BrandName { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public CarType CarType { get; set; }
        public DateTime ReleasedIn { get; set; }
    }
}
