using Entities;

namespace Repositories
{
    public class ProductsRepository : IRepository
    {
        private readonly ProductDbContext _dbContext;

        public ProductsRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _dbContext.Products;
        }
    }
}