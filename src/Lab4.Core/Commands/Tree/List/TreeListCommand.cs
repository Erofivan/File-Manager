using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Components;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.FileSystemDisplayers;
using Itmo.ObjectOrientedProgramming.Lab4.Core.OutputWriters;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Tree.List;

public sealed class TreeListCommand : ICommand
{
    private readonly int _depth;
    private readonly IOutputWriter _outputWriter;
    private readonly FileSystemTreeDisplaySettings _treeDisplaySettings;

    public TreeListCommand(
        int depth,
        IOutputWriter outputWriter,
        FileSystemTreeDisplaySettings treeDisplaySettings)
    {
        _depth = depth;
        _outputWriter = outputWriter;
        _treeDisplaySettings = treeDisplaySettings;
    }

    public CommandExecutionResult Execute(Context context)
    {
        if (context.IsConnected is false)
            return new CommandExecutionResult.FileSystemNotConnected();

        string currentFullPath = context.CurrentFullPath;

        if (context.FileSystem.DirectoryExists(currentFullPath) is false)
            return new CommandExecutionResult.DirectoryNotFound(context.CurrentPath);

        var visitor = new FileSystemTreeVisitor(_outputWriter, _treeDisplaySettings);

        foreach (IFileSystemComponent component in context.FileSystem.ListDirectory(currentFullPath, _depth))
            component.Accept(visitor);

        visitor.Flush();

        return new CommandExecutionResult.Success();
    }
}