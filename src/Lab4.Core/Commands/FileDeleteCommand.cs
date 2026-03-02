using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public sealed class FileDeleteCommand : ICommand
{
    private readonly string _path;

    public FileDeleteCommand(string path)
    {
        _path = path;
    }

    public CommandExecutionResult Execute(Context context)
    {
        if (context.FileSystem.IsConnected is false)
            return new CommandExecutionResult.FileSystemNotConnected();

        string resolvedPath = context.ResolvePath(_path);

        if (context.FileSystem.FileExists(resolvedPath) is false)
            return new CommandExecutionResult.FileNotFound(_path);

        FileModificationResult deleteOperationResult
            = context.FileSystem.DeleteFile(resolvedPath);

        return deleteOperationResult is FileModificationResult.Failure failure
            ? new CommandExecutionResult.Failure(failure.Message)
            : new CommandExecutionResult.Success();
    }
}