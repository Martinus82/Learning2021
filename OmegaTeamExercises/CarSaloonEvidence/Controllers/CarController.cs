using System;
using System.Linq;

using CarSaloonEvidence.CarModel;
using CarSaloonEvidence.Dtos;

using Microsoft.AspNetCore.Mvc;

using Repository.Abstraction;

namespace CarSaloonEvidence.Controllers
{
    [ApiController]
    [Route("api/v1/cars")]
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;

        public CarController(ICarRepository carRepository)
        {
            _carRepository = carRepository ?? throw new ArgumentNullException(nameof(carRepository));
        }

        [HttpPost]
        public IActionResult AddCar(CarDto newCar)
        {
            Manufacturer carManufacturer = _carRepository.GetManufacturers().FirstOrDefault(m => m.Name == newCar.ManufacturerName);

            bool manufacturerDoesNotExist = carManufacturer is null;
            if (manufacturerDoesNotExist)
            {
                carManufacturer = new Manufacturer
                {
                    Name = newCar.ManufacturerName
                };
            }

            // Map incoming Dto to domain model entity.
            // It usually happens in the application service layer.
            // TODO: consider extension method for mapping or use of kid of an automapper.
            Car car = new Car
            {
                BrandName = newCar.BrandName,
                CarType = newCar.CarType,
                ModelName = newCar.ModelName,
                ReleasedIn = newCar.ReleasedIn,
                Manufacturer = carManufacturer
            };

            return _carRepository.AddCar(car) is not null
                ? Ok(newCar)
                : BadRequest("The car was not added into repository!");
        }

        [HttpDelete]
        public IActionResult RemoveCar(Guid carId)
        {
            var removedCar = _carRepository.DeleteCar(carId);
            return removedCar is not null
                ? Ok(removedCar)
                : NotFound("Car not found.");
        }

        [HttpGet("{carId}")]
        public IActionResult GetCarById(Guid carId)
        {
            var car = _carRepository.GetCar(carId);
            return car is not null
                ? Ok(car)
                : NotFound();
        }

        [HttpPut("{carId}")]
        public IActionResult UpdateCar([FromRoute] Guid carId, [FromBody] CarDto carUpdate)
        {
            var carToBeUpdated = _carRepository.GetCar(carId);
            if (carToBeUpdated is null)
            {
                return NotFound("Car not found!");
            }

            Manufacturer carManufacturer = _carRepository.GetManufacturers().FirstOrDefault(m => m.Name == carUpdate.ManufacturerName);
            bool manufacturerDoesNotExist = carManufacturer is null;
            if (manufacturerDoesNotExist)
            {
                carManufacturer = new Manufacturer
                {
                    Name = carUpdate.ManufacturerName
                };
            }

            carToBeUpdated.CarType = carUpdate.CarType;
            carToBeUpdated.Manufacturer = carManufacturer;
            carToBeUpdated.BrandName = carUpdate.BrandName;
            carToBeUpdated.ModelName = carUpdate.ModelName;
            carToBeUpdated.ReleasedIn = carUpdate.ReleasedIn;

            var updatedCar = _carRepository.UpdateCar(carToBeUpdated);
            return updatedCar is not null
                ? Ok(updatedCar)
                : NotFound();
        }

        [HttpGet]
        public IActionResult GetAllCars()
        {
            return Ok(_carRepository.GetCars());
        }

        [HttpGet("manufacturers")]
        public IActionResult GetAllManufacturers()
        {
            return Ok(_carRepository.GetManufacturers());
        }

        [HttpGet("names")]
        public IActionResult GetCarsByManufacturerName([FromQuery] string brand)
        {
            return Ok(_carRepository.GetCars().Where(car => car.BrandName == brand));
        }

        [HttpGet("manufacturers/{manufacturerId}")]
        public IActionResult GetAllCarsByManufacturerId(int manufacturerId)
        {
            return Ok(_carRepository.GetCars().Where(car => car.Manufacturer.Id == manufacturerId));
        }

        [HttpGet("{manufacturerId}/types/{carType}")]
        public IActionResult GetAllCarsTypesByManufacturerIdAndCarType(int manufacturerId, int carType)
        {
            return Ok(_carRepository.GetCars().Where(car => car.Manufacturer.Id == manufacturerId && car.CarType == (CarType)carType));
        }

        [HttpGet("{manufacturerId}/types/{carType}/released-in")]
        public IActionResult GetAllCarsTypesByManufacturerIdAndCarTypeReleasedIn(int manufacturerId, int carType, [FromQuery] DateTime releasedIn)
        {
            return Ok(_carRepository.GetCars().Where(car => car.Manufacturer.Id == manufacturerId && car.CarType == (CarType)carType && car.ReleasedIn >= releasedIn));
        }
    }
}
