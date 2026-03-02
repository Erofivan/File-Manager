using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public sealed class FileCopyCommand : ICommand
{
    private readonly string _sourcePath;
    private readonly string _destinationPath;

    public FileCopyCommand(string sourcePath, string destinationPath)
    {
        _sourcePath = sourcePath;
        _destinationPath = destinationPath;
    }

    public CommandExecutionResult Execute(Context context)
    {
        if (context.FileSystem.IsConnected is false)
            return new CommandExecutionResult.FileSystemNotConnected();

        string resolvedSourcePath = context.ResolvePath(_sourcePath);
        string resolvedDestinationPath = context.ResolvePath(_destinationPath);

        if (context.FileSystem.FileExists(resolvedSourcePath) is false)
            return new CommandExecutionResult.FileNotFound(_sourcePath);

        if (context.FileSystem.DirectoryExists(resolvedSourcePath) is false)
            return new CommandExecutionResult.DirectoryNotFound(_destinationPath);

        FileModificationResult copyOperationResult =
            context.FileSystem.CopyFile(resolvedSourcePath, resolvedDestinationPath);

        return copyOperationResult is FileModificationResult.Failure failure
            ? new CommandExecutionResult.Failure(failure.Message)
            : new CommandExecutionResult.Success();
    }
}