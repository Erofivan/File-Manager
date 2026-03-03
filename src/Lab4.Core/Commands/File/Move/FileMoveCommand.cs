using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.FileSystemOperationsResults;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Move;

public sealed class FileMoveCommand : ICommand
{
    private readonly string _sourcePath;
    private readonly string _destinationPath;

    public FileMoveCommand(string sourcePath, string destinationPath)
    {
        _sourcePath = sourcePath;
        _destinationPath = destinationPath;
    }

    public CommandExecutionResult Execute(Context context)
    {
        if (context.IsConnected is false)
            return new CommandExecutionResult.FileSystemNotConnected();

        string resolvedSourcePath = context.ResolvePath(_sourcePath);
        string resolvedDestinationPath = context.ResolvePath(_destinationPath);

        if (context.FileSystem.FileExists(resolvedSourcePath) is false)
            return new CommandExecutionResult.FileNotFound(_sourcePath);

        if (context.FileSystem.DirectoryExists(resolvedDestinationPath) is false)
            return new CommandExecutionResult.DirectoryNotFound(_destinationPath);

        FileMoveOperationResult moveResult =
            context.FileSystem.MoveFile(resolvedSourcePath, resolvedDestinationPath);

        return moveResult is FileMoveOperationResult.Failure failure
            ? new CommandExecutionResult.Failure(failure.Message)
            : new CommandExecutionResult.Success();
    }
}