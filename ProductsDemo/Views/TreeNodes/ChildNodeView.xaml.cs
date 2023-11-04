using ProductsDemo.Models;

namespace ProductsDemo.Views.TreeNodes;

public partial class ChildNodeView : ITreeNode 
{
    public ChildNodeView() => InitializeComponent();
    
    public string Path => "Parent Node/Child Node";
}