using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;
using CarSaloonEvidence.CarModel;
using Repository.Abstraction;

namespace MockCarRepositories
{
    public class JsonFileCarRepository : ICarRepository
    {
        private readonly List<Car> _cars = new();

        public JsonFileCarRepository()
        {
        }

        public bool AddCar(Car newCar)
        {
            // Store newCar to json file repository.
            throw new System.NotImplementedException();
        }

        public IEnumerable<Car> GetCars()
        {
            _cars.AddRange(ReadJsonFileContent());
            return _cars;
        }

        public IEnumerable<Manufacturer> GetManufacturers()
        {
            return Manufacturer.GetManufacturers();
        }

        public IEnumerable<Car> ReadJsonFileContent()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string content = File.ReadAllText(path + "\\Repository\\Cars.json");
            var cars = JsonSerializer.Deserialize<List<Car>>(content);
            return cars;
        }
    }
}
