using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Components;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Trees;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public sealed class TreeListCommand : ICommand
{
    private readonly int _depth;

    public TreeListCommand(int depth)
    {
        _depth = depth;
    }

    public CommandResult Execute(ExecutionContext context)
    {
        if (context.FileSystem.IsConnected is false)
            return new CommandResult.FileSystemNotConnected();

        string currentFullPath = context.ConnectionPath.TrimEnd('/') + context.CurrentPath;

        if (context.FileSystem.DirectoryExists(currentFullPath) is false)
            return new CommandResult.DirectoryNotFound(context.CurrentPath);

        var visitor = new FileSystemTreeVisitor(context.OutputWriter, context.TreeDisplaySettings);

        foreach (IFileSystemComponent component in context.FileSystem.ListDirectory(currentFullPath, _depth))
            component.Accept(visitor);

        visitor.Flush();

        return new CommandResult.Success();
    }
}