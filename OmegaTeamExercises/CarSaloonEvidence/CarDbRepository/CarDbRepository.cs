using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using CarSaloonEvidence.CarModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repository.Abstraction;

namespace CarDbRepository
{
    public class CarDbRepository : ICarRepository
    {
        private readonly CarDbContext _carDbContext;
        private readonly ILogger<CarDbContext> _logger;

        public CarDbRepository(CarDbContext carDbContext, ILogger<CarDbContext> logger)
        {
            _carDbContext = carDbContext ?? throw new ArgumentNullException(nameof(carDbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IEnumerable<Car> GetCars()
        {
            var cars = _carDbContext.Cars.Include(c => c.Manufacturer).ToList();
            _logger.LogInformation($"Requested: {JsonSerializer.Serialize(cars)}");
            return cars;
        }

        public Car AddCar(Car newCar)
        {
            var added = _carDbContext.Cars.Add(newCar);
            _carDbContext.SaveChanges();
            _logger.LogInformation($"Added: {JsonSerializer.Serialize(added)}");
            return added.Entity;
        }

        public IEnumerable<Manufacturer> GetManufacturers()
        {
            var manufacturers = _carDbContext.Manufacturers.ToList();
            _logger.LogInformation($"Requested manufacturers: {JsonSerializer.Serialize(manufacturers)}");
            return manufacturers;
        }

        public Car DeleteCar(Guid carId)
        {
            var removed = _carDbContext.Cars.FirstOrDefault(c => c.Id == carId);
            _logger.LogInformation($"Deleted: {JsonSerializer.Serialize(removed)}");
            _carDbContext.SaveChanges();
            return removed;
        }

        public Car GetCar(Guid carId)
        {
            var car = _carDbContext.Cars.FirstOrDefault(c => c.Id == carId);
            _logger.LogInformation($"Requested car: {JsonSerializer.Serialize(car)}");
            return car;
        }

        public Car UpdateCar(Car car)
        {
            var updatedCar = _carDbContext.Update(car);
            _carDbContext.SaveChanges();
            _logger.LogInformation($"Updated car: {JsonSerializer.Serialize(updatedCar)}");
            return updatedCar.Entity;
        }
    }
}
