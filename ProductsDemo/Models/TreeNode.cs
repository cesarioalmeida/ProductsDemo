using System.Collections.Generic;

namespace ProductsDemo.Models;

public class TreeNode
{
    public string Header { get; set; } = string.Empty;
    public string? View { get; set; } = null;
    public List<TreeNode> Children { get; } = new();
}