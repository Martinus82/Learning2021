using System;
using CarSaloonEvidence.CarModel;

namespace CarSaloonEvidence.Dtos
{
    public class CarDto
    {
        public string ModelName { get; set; }
        public string BrandName { get; set; }
        public string ManufacturerName { get; set; }
        public CarType CarType { get; set; }
        public DateTime ReleasedIn { get; set; }
    }
}