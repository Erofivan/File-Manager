namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

public abstract record FileOperationResult
{
    private FileOperationResult() { }

    public sealed record Success : FileOperationResult;

    public sealed record Failure(string Message) : FileOperationResult;
}