using DevExpress.Mvvm;
using ProductsDemo.Core.Models;

namespace ProductsDemo.Models;

public class CustomerModel : BindableBase
{
    public CustomerModel(Customer customer) 
        => Customer = customer;

    public Customer Customer { get; }

    public string? ContactName
    {
        get => Customer.ContactName;
        set => SetValue(value);
    }

    public string? City
    {
        get => Customer.City;
        set => SetValue(value);
    }
}