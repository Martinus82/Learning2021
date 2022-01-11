using Entities;

using Microsoft.AspNetCore.Mvc;

using Repositories;

namespace SqLiteExampleWebApiV2.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;
    private readonly IRepository _repository;

    public ProductController(ILogger<ProductController> logger, IRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    [HttpGet(Name = "GetProduct")]
    public IEnumerable<Product> Get()
    {
        return _repository.GetProducts();
    }
}
