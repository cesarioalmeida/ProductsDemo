using System;
using System.Windows;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using Microsoft.Extensions.DependencyInjection;
using ProductsDemo.Core.Services;
using ProductsDemo.Core.UI;
using ProductsDemo.Services;
using ProductsDemo.ViewModels;
using ProductsDemo.Views;

namespace ProductsDemo;

public class ProductDemoBootstrapper : Bootstrapper
{
    public ProductDemoBootstrapper(Application application, string title) : base(application, title)
    {
    }

    protected override void ConfigureServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddDbContext<NorthwindContext>();
        serviceCollection.AddSingleton<ICustomerService, CustomerService>();
        serviceCollection.AddScoped<INavigationService, NavigationService>();
        
        serviceCollection.AddScopedViewModel<MainWindowViewModel>();
        serviceCollection.AddScopedViewModel<CustomersViewModel>();

        serviceCollection.AddScoped<CustomersView>();
        serviceCollection.AddScoped<ProductsView>();

        serviceCollection.AddSingleton<MainWindow>();
        
        DependencyInjectionSource.Resolver = Resolve;
    }

    private static object? Resolve(Type? type, object key, string name) => type == null ? null : ServiceProvider.GetService(type);

    protected override void ShowWindow()
        => ServiceProvider.GetRequiredService<MainWindow>().Show();
}