namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.FileSystemOperationsResults;

public abstract record FileReadOperationResult
{
    private FileReadOperationResult() { }

    public sealed record Success(string Content) : FileReadOperationResult;

    public sealed record Failure(string Message) : FileReadOperationResult;
}