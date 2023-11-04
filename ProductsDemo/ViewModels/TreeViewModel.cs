using System.Collections.Generic;
using System.Linq;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using ProductsDemo.Models;

namespace ProductsDemo.ViewModels;

[POCOViewModel]
public class TreeViewModel
{
    private readonly ILogger<TreeViewModel> _logger;
    
    public TreeViewModel(ILogger<TreeViewModel> logger, IEnumerable<ITreeNode> nodes)
    {
        _logger = logger;
        Nodes = GetNodes(nodes).ToList();
    }
    
    public virtual bool IsLoading { get; set; }
    
    public virtual List<TreeNode> Nodes { get; } = new();
    
    [ServiceProperty(SearchMode=ServiceSearchMode.PreferParents)]
    protected virtual IDocumentManagerService DocumentManagerService { get; } = null!;
    
    [UsedImplicitly]
    public void OpenNode(TreeNode? node)
    {
        if (node?.View is null)
        {
            return;
        }

        var doc = DocumentManagerService.CreateDocument(node.View, null, this);
        doc?.Show();
    }

    // gets the tree based on the Location property of each node
    private IEnumerable<TreeNode> GetNodes(IEnumerable<ITreeNode> items)
    {
        var nodes = new List<TreeNode>();
        
        // assumes the path is in the format "Parent1/Parent2/.../Header"
        foreach (var item in items)
        {
            var path = item.Path.Split('/');
            var current = nodes;
            for (var i = 0; i < path.Length - 1; i++)
            {
                var header = path[i];
                var node = current.FirstOrDefault(x => x.Header == header);
                if (node is null)
                {
                    node = new TreeNode { Header = header };
                    current.Add(node);
                }

                current = node.Children;
            }

            current.Add(new TreeNode { Header = path[^1], View = item.GetType().Name });
        }
        
        return nodes;
    }
}