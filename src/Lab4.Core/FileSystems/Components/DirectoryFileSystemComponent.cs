namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Components;

public sealed class DirectoryFileSystemComponent : IFileSystemComponent
{
    public DirectoryFileSystemComponent(string name, IEnumerable<IFileSystemComponent> children)
    {
        Name = name;
        Children = children;
    }

    public string Name { get; }

    public IEnumerable<IFileSystemComponent> Children { get;  }

    public void Accept(IFileSystemComponentsVisitor visitor)
    {
        visitor.Visit(this);
    }
}