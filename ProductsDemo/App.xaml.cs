using System.Windows;

namespace ProductsDemo
{
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bootstrapper = new ProductDemoBootstrapper(this, "Products Demo");
            bootstrapper.Run();
        }
    }
}
