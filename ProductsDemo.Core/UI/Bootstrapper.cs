using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ProductsDemo.Core.UI;

public abstract class Bootstrapper : IBootstrapper
{
    private readonly Application _application;
    private readonly IHost _appHost;
    private readonly ILogger<Bootstrapper> _logger;
    private readonly string _title;

    protected Bootstrapper(Application application, string title)
    {
        _application = application;
        _title = title;

        _appHost = Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) => { ConfigureServicesInternal(services); })
            .Build();

        ServiceProvider = _appHost.Services;
        _logger = _appHost.Services.GetRequiredService<ILogger<Bootstrapper>>();
    }

    public void Run()
    {
        _application.DispatcherUnhandledException += AppDispatcherUnhandledException;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;
        TaskScheduler.UnobservedTaskException += TaskSchedulerUnobservedTaskException;
        _application.Exit += (_, _) => OnExit();

        LogStartup();

        ShowWindow();
    }

    public static IServiceProvider ServiceProvider { get; private set; }
    
    protected abstract void ConfigureServices(IServiceCollection serviceCollection);

    protected abstract void ShowWindow();
    
    public void Shutdown(string reason)
    {
        try
        {
            _logger.LogInformation("Application shutting down: {Reason}", reason);
            _application.Shutdown();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error shutting down application");
        }
    }
    
    private void ConfigureServicesInternal(IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IBootstrapper>(this);
        ConfigureServices(serviceCollection);
    }

    private void AppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        => HandleException(e.Exception);

    private void TaskSchedulerUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        => HandleException(e.Exception);

    private void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        => HandleException(e.ExceptionObject as Exception ?? new Exception("Unknown exception"));

    private void HandleException(Exception exception)
    {
        _logger.LogError("Exception raised at: {Now}", DateTime.Now);
        _logger.LogError("Exception Message  : {ExceptionMessage}", exception.Message);
    }

    private void LogStartup()
    {
        _logger.LogInformation("Application : {Title}", _title);
        _logger.LogInformation("Version     : {AssemblyVersionNumber}", GetType().Assembly.GetName().Version);
    }

    private void OnExit()
    {
        _logger.LogInformation("Application exiting");
        _appHost.Dispose();
    }
}