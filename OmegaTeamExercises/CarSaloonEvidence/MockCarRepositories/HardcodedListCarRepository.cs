using System;
using System.Collections.Generic;
using System.Linq;
using CarSaloonEvidence.CarModel;
using Repository.Abstraction;

namespace MockCarRepositories
{
    public class HardcodedListCarRepository : ICarRepository
    {
        private readonly List<Car> _cars = new();
        private readonly ICollection<Manufacturer> _manufacturers;

        public HardcodedListCarRepository()
        {
            _manufacturers = Manufacturer.GetManufacturers();
            AddCarsToList();
        }

        public bool AddCar(Car newCar)
        {
            _cars.Add(newCar);
            return true;
        }

        public IEnumerable<Car> GetCars()
        {
            return _cars;
        }

        public IEnumerable<Manufacturer> GetManufacturers()
        {
            return Manufacturer.GetManufacturers();
        }

        private void AddCarsToList()
        {
            _cars.Add(new Car
            {
                ModelName = "Scala",
                BrandName = "Skoda",
                CarType = CarType.Hatchback,
                Manufacturer = _manufacturers.Single(m => m.Name == "Skoda"),
                ReleasedIn = new DateTime(2019, 1, 1)
            });

            _cars.Add(new Car
            {
                ModelName = "Octavia",
                BrandName = "Skoda",
                CarType = CarType.LiftBack,
                Manufacturer = _manufacturers.Single(m => m.Name == "Skoda"),
                ReleasedIn = new DateTime(2010, 1, 1)
            });

            _cars.Add(new Car
            {
                ModelName = "Octavia",
                BrandName = "Skoda",
                CarType = CarType.Wagon,
                Manufacturer = _manufacturers.Single(m => m.Name == "Skoda"),
                ReleasedIn = new DateTime(2015, 1, 1)
            });

            _cars.Add(new Car
            {
                ModelName = "Octavia",
                BrandName = "Skoda",
                CarType = CarType.Wagon,
                Manufacturer = _manufacturers.Single(m => m.Name == "Skoda"),
                ReleasedIn = new DateTime(2016, 1, 1)
            });

            _cars.Add(new Car
            {
                ModelName = "i30",
                BrandName = "Hyundai",
                CarType = CarType.Wagon,
                Manufacturer = _manufacturers.Single(m => m.Name == "Hyundai"),
                ReleasedIn = new DateTime(2018, 1, 1)
            });

            _cars.Add(new Car
            {
                ModelName = "i20",
                BrandName = "Hyundai",
                CarType = CarType.Hatchback,
                Manufacturer = _manufacturers.Single(m => m.Name == "Hyundai"),
                ReleasedIn = new DateTime(2017, 1, 1)
            });

            _cars.Add(new Car
            {
                ModelName = "i10",
                BrandName = "Hyundai",
                CarType = CarType.Sedan,
                Manufacturer = _manufacturers.Single(m => m.Name == "Hyundai"),
                ReleasedIn = new DateTime(2018, 1, 1)
            });

            _cars.Add(new Car
            {
                ModelName = "Ceed",
                BrandName = "Kia",
                CarType = CarType.LiftBack,
                Manufacturer = _manufacturers.Single(m => m.Name == "Kia"),
                ReleasedIn = new DateTime(2013, 1, 1)
            });

            _cars.Add(new Car
            {
                ModelName = "Ceed",
                BrandName = "Kia",
                CarType = CarType.Wagon,
                Manufacturer = _manufacturers.Single(m => m.Name == "Kia"),
                ReleasedIn = new DateTime(2014, 1, 1)
            });

            _cars.Add(new Car
            {
                ModelName = "ProCeed",
                BrandName = "Kia",
                CarType = CarType.Sedan,
                Manufacturer = _manufacturers.Single(m => m.Name == "Kia"),
                ReleasedIn = new DateTime(2011, 1, 1)
            });

            _cars.Add(new Car
            {
                ModelName = "Ram",
                BrandName = "Dodge",
                CarType = CarType.PickUp,
                Manufacturer = _manufacturers.Single(m => m.Name == "Dodge"),
                ReleasedIn = new DateTime(2020, 1, 1)
            });
        }
    }
}
