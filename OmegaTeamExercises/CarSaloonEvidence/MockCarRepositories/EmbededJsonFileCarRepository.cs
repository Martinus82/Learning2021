using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using CarSaloonEvidence.CarModel;
using Repository.Abstraction;

namespace MockCarRepositories
{
    public class EmbededJsonFileCarRepository : ICarRepository
    {
        private readonly List<Car> _cars = new();

        public EmbededJsonFileCarRepository()
        {
        }

        public bool AddCar(Car newCar)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Car> GetCars()
        {
            _cars.AddRange(ReadEmbededContent());
            return _cars;
        }

        public IEnumerable<Manufacturer> GetManufacturers()
        {
            return Manufacturer.GetManufacturers();
        }

        public IEnumerable<Car> ReadEmbededContent()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var embededJsonFile = assembly.GetManifestResourceNames().Single(file => file.EndsWith("JsonFileDb.json"));

            using Stream stream = assembly.GetManifestResourceStream(embededJsonFile);
            using StreamReader reader = new StreamReader(stream);
            string content = reader.ReadToEnd();

            var cars = JsonSerializer.Deserialize<List<Car>>(content);
            return cars;
        }
    }
}
