using ProductsDemo.Models;

namespace ProductsDemo.Views.TreeNodes;

public partial class ParentNodeView : ITreeNode 
{
    public ParentNodeView() => InitializeComponent();

    public string Path => "Parent Node";
}