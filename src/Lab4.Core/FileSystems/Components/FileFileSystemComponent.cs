namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Components;

public sealed class FileFileSystemComponent : IFileSystemComponent
{
    public FileFileSystemComponent(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public void Accept(IFileSystemComponentsVisitor visitor)
    {
        visitor.Visit(this);
    }
}