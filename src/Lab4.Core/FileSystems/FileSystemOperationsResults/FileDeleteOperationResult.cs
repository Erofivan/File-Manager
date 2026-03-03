namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.FileSystemOperationsResults;

public abstract record FileDeleteOperationResult
{
    private FileDeleteOperationResult() { }

    public sealed record Success : FileDeleteOperationResult;

    public sealed record Failure(string Message) : FileDeleteOperationResult;
}