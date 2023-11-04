using ProductsDemo.Models;

namespace ProductsDemo.Views.TreeNodes;

public partial class ChildNodeOtherParentView : ITreeNode 
{
    public ChildNodeOtherParentView() => InitializeComponent();
    
    public string Path => "Root Parent/Child Level 1/Child Level 2";
}