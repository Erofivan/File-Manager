using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public sealed class FileRenameCommand : ICommand
{
    private readonly string _path;
    private readonly string _newName;

    public FileRenameCommand(string path, string newName)
    {
        _path = path;
        _newName = newName;
    }

    public CommandExecutionResult Execute(Context context)
    {
        if (context.FileSystem.IsConnected is false)
            return new CommandExecutionResult.FileSystemNotConnected();

        if (_newName.Contains('/', StringComparison.Ordinal))
            return new CommandExecutionResult.Failure("New name must contain path separators");

        string resolvedPath = context.ResolvePath(_path);

        if (context.FileSystem.FileExists(resolvedPath) is false)
            return new CommandExecutionResult.FileNotFound(_path);

        FileModificationResult renameOperationResult =
            context.FileSystem.RenameFile(resolvedPath, _newName);

        return renameOperationResult is FileModificationResult.Failure failure
            ? new CommandExecutionResult.Failure(failure.Message)
            : new CommandExecutionResult.Success();
    }
}