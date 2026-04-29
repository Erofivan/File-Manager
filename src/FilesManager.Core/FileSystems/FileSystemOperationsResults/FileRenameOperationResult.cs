namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.FileSystemOperationsResults;

public abstract record FileRenameOperationResult
{
    private FileRenameOperationResult() { }

    public sealed record Success : FileRenameOperationResult;

    public sealed record Failure(string Message) : FileRenameOperationResult;
}