using System;
using System.Windows;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using Microsoft.Extensions.DependencyInjection;
using ProductsDemo.Core.Services;
using ProductsDemo.Core.UI;
using ProductsDemo.Models;
using ProductsDemo.Services;
using ProductsDemo.ViewModels;
using ProductsDemo.Views;
using ProductsDemo.Views.TreeNodes;

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
        
        serviceCollection.AddScoped<TreeView>();
        serviceCollection.AddScopedViewModel<TreeViewModel>();
        
        serviceCollection.AddScoped<PortfolioView>();
        serviceCollection.AddScoped<ITreeNode, PortfolioView>();
        serviceCollection.AddScoped<NettingPoolView>();
        serviceCollection.AddScoped<ITreeNode, NettingPoolView>();
        serviceCollection.AddScoped<ParentNodeView>();
        serviceCollection.AddScoped<ITreeNode, ParentNodeView>();
        serviceCollection.AddScoped<ChildNodeView>();
        serviceCollection.AddScoped<ITreeNode, ChildNodeView>();
        serviceCollection.AddScoped<ChildNodeOtherParentView>();
        serviceCollection.AddScoped<ITreeNode, ChildNodeOtherParentView>();

        serviceCollection.AddSingleton<MainWindow>();
        
        DependencyInjectionSource.Resolver = Resolve;
    }

    private static object? Resolve(Type? type, object key, string name) => type == null ? null : ServiceProvider.GetService(type);

    protected override void ShowWindow()
        => ServiceProvider.GetRequiredService<MainWindow>().Show();
}