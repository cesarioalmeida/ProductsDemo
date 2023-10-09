using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DevExpress.Mvvm;
using Microsoft.Extensions.DependencyInjection;
using ProductsDemo.Core.Extensions;
using ProductsDemo.Core.UI;

namespace ProductsDemo.Services;

public class NavigationService : BindableBase, INavigationService
{
    private readonly Dictionary<string, NavigationElement> _navigationCache = new();
    private string _currentView = string.Empty;

    public bool CanNavigate => true;

    public bool CanGoBack => _navigationCache.Count > 1;

    public bool CanGoForward => _navigationCache.Count > 1 &&
                                _navigationCache.Count > _navigationCache.IndexOfKey(_currentView) + 1;

    public object? Current { get; private set; }

    public event EventHandler? CanGoBackChanged;
    public event EventHandler? CanGoForwardChanged;
    public event EventHandler? CurrentChanged;

    public void ClearCache()
    {
        foreach (var element in _navigationCache)
        {
            _navigationCache[element.Key] = element.Value with {ViewModel = null};
        }
    }

    public void ClearNavigationHistory()
    {
        _navigationCache.Clear();
        _currentView = string.Empty;
        Current = null!;

        CanGoBackChanged?.Invoke(this, EventArgs.Empty);
        CanGoForwardChanged?.Invoke(this, EventArgs.Empty);
        CurrentChanged?.Invoke(this, EventArgs.Empty);
    }

    public void Navigate(string viewAsString, object viewModel, object param, object parentViewModel, bool saveToJournal)
    {
        if (string.IsNullOrEmpty(viewAsString))
        {
            throw new ArgumentException("ViewType cannot be null or empty.", nameof(viewAsString));
        }

        if (!_navigationCache.ContainsKey(viewAsString))
        {
            _navigationCache.Add(viewAsString, new NavigationElement(viewModel, param, parentViewModel));
        }

        // create a new instance of the view
        var viewType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == viewAsString);

        if (viewType is null)
        {
            throw new ArgumentException("ViewType not found.", nameof(viewAsString));
        }
        
        var view = Bootstrapper.ServiceProvider.GetRequiredService(viewType);
        
        _currentView = viewAsString;
        Current = view;

        CanGoBackChanged?.Invoke(this, EventArgs.Empty);
        CanGoForwardChanged?.Invoke(this, EventArgs.Empty);
        CurrentChanged?.Invoke(this, EventArgs.Empty);
        
        RaisePropertyChanged(() => Current);
    }

    public void GoBack(object param)
    {
        var index = _navigationCache.IndexOfKey(_currentView) - 1;
        NavigateInternal(index, param);
    }

    public void GoForward(object param)
    {
        var index = _navigationCache.IndexOfKey(_currentView) + 1;
        NavigateInternal(index, param);
    }

    public void GoBack()
        => GoBack(null!);

    public void GoForward()
        => GoForward(null!);

    private void NavigateInternal(int index, object? param = null)
    {
        Navigate(
            _navigationCache.ElementAt(index).Key,
            _navigationCache.ElementAt(index).Value.ViewModel!,
            param ?? _navigationCache.ElementAt(index).Value.Parameter!,
            _navigationCache.ElementAt(index).Value.ParentViewModel!,
            false);
    }

    private record NavigationElement(object? ViewModel, object? Parameter, object? ParentViewModel);
}