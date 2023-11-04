using ProductsDemo.Models;

namespace ProductsDemo.Views.TreeNodes;

public partial class PortfolioView : ITreeNode 
{
    public PortfolioView() => InitializeComponent();

    public string Path => "Portfolio";
}