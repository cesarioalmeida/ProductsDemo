using DevExpress.Mvvm.POCO;
using Microsoft.Extensions.DependencyInjection;

namespace ProductsDemo;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddScopedViewModel<TViewModel>(this IServiceCollection serviceCollection)
        where TViewModel : class
    {
        serviceCollection.AddScoped(typeof(TViewModel), ViewModelSource.GetPOCOType(typeof(TViewModel)));
        return serviceCollection;
    }
}