using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.FileSystemOperationsResults;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Rename;

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
        if (context.IsConnected is false)
            return new CommandExecutionResult.FileSystemNotConnected();

        string resolvedPath = context.ResolvePath(_path);

        if (context.FileSystem.FileExists(resolvedPath) is false)
            return new CommandExecutionResult.FileNotFound(_path);

        FileRenameOperationResult renameResult =
            context.FileSystem.RenameFile(resolvedPath, _newName);

        return renameResult is FileRenameOperationResult.Failure failure
            ? new CommandExecutionResult.Failure(failure.Message)
            : new CommandExecutionResult.Success();
    }
}