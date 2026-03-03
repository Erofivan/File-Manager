namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.FileSystemOperationsResults;

public abstract record FileCopyOperationResult
{
    private FileCopyOperationResult() { }

    public sealed record Success : FileCopyOperationResult;

    public sealed record Failure(string Message) : FileCopyOperationResult;
}