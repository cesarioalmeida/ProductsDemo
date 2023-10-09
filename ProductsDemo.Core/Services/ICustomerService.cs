using ProductsDemo.Core.Models;

namespace ProductsDemo.Core.Services;

public interface ICustomerService
{
    IAsyncEnumerator<Customer> GetCustomers();
}