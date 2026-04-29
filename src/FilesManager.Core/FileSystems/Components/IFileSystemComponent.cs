namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Components;

public interface IFileSystemComponent
{
    string Name { get; }

    void Accept(IFileSystemComponentsVisitor visitor);
}