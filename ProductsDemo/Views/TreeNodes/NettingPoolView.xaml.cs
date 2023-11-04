using ProductsDemo.Models;

namespace ProductsDemo.Views.TreeNodes;

public partial class NettingPoolView : ITreeNode 
{
    public NettingPoolView() => InitializeComponent();

    public string Path => "Netting Pool";
}