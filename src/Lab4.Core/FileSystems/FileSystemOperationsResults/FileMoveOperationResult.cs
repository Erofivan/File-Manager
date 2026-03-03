namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.FileSystemOperationsResults;

public abstract record FileMoveOperationResult
{
    private FileMoveOperationResult() { }

    public sealed record Success : FileMoveOperationResult;

    public sealed record Failure(string Message) : FileMoveOperationResult;
}