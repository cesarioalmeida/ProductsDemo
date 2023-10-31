using System.Collections.ObjectModel;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductsDemo.Core.Services;
using ProductsDemo.Models;
using ProductsDemo.Views;

namespace ProductsDemo.ViewModels;

[POCOViewModel]
public class CustomersViewModel
{
    private readonly NorthwindContext _db;
    private readonly ILogger<CustomersViewModel> _logger;

    public CustomersViewModel(ILogger<CustomersViewModel> logger, NorthwindContext db)
    {
        _logger = logger;
        _db = db;
    }

    public virtual ObservableCollection<CustomerModel> Customers { get; } = new();

    public virtual CustomerModel? SelectedCustomer { get; set; }

    public virtual bool IsLoading { get; set; }

    protected IDocumentManagerService DocumentManagerService => this.GetService<IDocumentManagerService>();

    [UsedImplicitly]
    public async Task Load()
    {
        if (IsLoading)
        {
            _logger.LogWarning("Already loading customers");
            return;
        }

        IsLoading = true;
        _logger.LogInformation("Loading customers");
        Customers.Clear();
        await _db.Customers.ForEachAsync(x => Customers.Add(new CustomerModel(x)));
        _logger.LogInformation("Loaded {Count} customers", Customers.Count);
        IsLoading = false;
    }

    [UsedImplicitly]
    public void OpenCustomer(CustomerModel? model)
    {
        if (model is null)
        {
            return;
        }

        var doc = DocumentManagerService.CreateDocument(nameof(CustomerView), model);
        doc?.Show();
    }
}