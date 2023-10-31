using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using ProductsDemo.Core.Services;

namespace ProductsDemo.ViewModels;

[POCOViewModel]
public class MainWindowViewModel
{
    private readonly ICustomerService _customerService;

    public MainWindowViewModel(ICustomerService customerService, INavigationService navigationService)
    {
        _customerService = customerService;
        NavigationService = navigationService;
    }
    
    public INavigationService NavigationService { get; }

    public void Navigate(string target) 
        => NavigationService.Navigate(target, null, this);
}