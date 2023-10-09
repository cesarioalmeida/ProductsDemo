using Microsoft.Extensions.Logging;
using ProductsDemo.Core.Models;

namespace ProductsDemo.Core.Services;

public class CustomerService : ICustomerService
{
    private readonly ILogger<NorthwindContext> _logger;
    private readonly NorthwindContext _context;

    public CustomerService(ILogger<NorthwindContext> logger, NorthwindContext context)
        => (_logger, _context) = (logger, context);

    public IAsyncEnumerator<Customer> GetCustomers()
    {
        _logger.LogInformation("Getting customers");
        return _context.Customers.GetAsyncEnumerator();
    }
}