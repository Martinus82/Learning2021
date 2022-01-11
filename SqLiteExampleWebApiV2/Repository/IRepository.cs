using Entities;

namespace Repositories
{
    public interface IRepository
    {
        public IEnumerable<Product> GetProducts();
    }
}