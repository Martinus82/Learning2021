using System;
using System.Collections.Generic;
using CarSaloonEvidence.CarModel;

namespace Repository.Abstraction
{
    public interface ICarRepository
    {
        public IEnumerable<Car> GetCars();

        /// <summary>
        /// Adds the car into the repository.
        /// </summary>
        /// <param name="car">The car to add to the repository.</param>
        /// <returns>True if car was added, otherwise false.</returns>
        Car AddCar(Car car);
        IEnumerable<Manufacturer> GetManufacturers();
        Car DeleteCar(Guid carId);
        Car GetCar(Guid carId);
        Car UpdateCar(Car car);
    }
}