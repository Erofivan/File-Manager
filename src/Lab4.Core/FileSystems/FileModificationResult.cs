namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

public abstract record FileModificationResult
{
    private FileModificationResult() { }

    public sealed record Success : FileModificationResult;

    public sealed record Failure(string Message) : FileModificationResult;
}