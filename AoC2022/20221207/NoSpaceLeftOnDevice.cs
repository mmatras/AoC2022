using System.ComponentModel;

namespace AoC2022._20221207;

public enum NodeType {F, D}

public record TreeNode(NodeType NodeType, string Name, DirNode? Parent);
public record FileNode (string Name, DirNode? Parent, int Size) : TreeNode(NodeType.F, Name, Parent);

public record DirNode(string Name, DirNode? Parent) : TreeNode(NodeType.D, Name, Parent)
{
    public HashSet<TreeNode> Childrens = new();
}

public static class NoSpaceLeftOnDevice
{
    public static void Run()
    {
        var lines = File.ReadAllLines("20221207/input.txt").Skip(1).ToList();

        var root = new DirNode("root", null);
        ParseCommands(lines, 0, root);

        var dirSize = SumDirSize(root).ToList();
        
        Console.WriteLine(dirSize.Where(s => s < 100000).Sum());

        var neededSpace = 30_000_000 - (70_000_000 - dirSize.First());

        Console.WriteLine(dirSize.Where(d => d > neededSpace).Min());
    }

    private static IEnumerable<int> SumDirSize(TreeNode node)
    {
        var dirNode = node as DirNode;
        var dirsSum = dirNode.Childrens.Where(ch => ch.NodeType == NodeType.D).Select(SumDirSize).ToList();
        var result = new List<int>
        {
            dirNode.Childrens.Where(ch => ch.NodeType == NodeType.F).Sum(ch => (ch as FileNode).Size) + dirsSum.Select(m => m.First()).Sum()
        };
        result.AddRange(dirsSum.SelectMany(m => m));
        return result;
    }
    
    private static TreeNode ParseCommands(IList<string> lines, int idx, DirNode parent) => lines[idx] switch
    {
        { } when lines.Count - 1 == idx => CreateFileNode(lines[idx], parent),
        { } when lines[idx] == "$ ls" => ParseCommands(lines, idx+ 1, parent),
        { } when lines[idx] == "$ cd .." => ParseCommands(lines, idx+ 1, parent.Parent),
        { } when lines[idx].StartsWith("$ cd ") => ParseCommands(lines, idx+ 1, parent.Childrens.First(ch => ch.Name == lines[idx].Substring(5)) as DirNode),
        { } when char.IsDigit(lines[idx][0]) => ParseCommands(lines, idx + 1, CreateFileNode(lines[idx], parent)),
        { } when lines[idx].StartsWith("dir") => ParseCommands(lines, idx + 1, CreateDirNode(lines[idx], parent)), 
    };

    private static DirNode CreateFileNode(string line, DirNode? parent)
    {
        var newNode = new FileNode(line.Split()[1], parent,
            int.Parse(line.Split()[0]));
        parent?.Childrens.Add(newNode);
        return parent;
    }
    
    private static DirNode CreateDirNode(string line, DirNode? parent)
    {
        var newNode = new DirNode(line.Split()[1], parent);
        parent?.Childrens.Add(newNode);
        return parent;
    }
}