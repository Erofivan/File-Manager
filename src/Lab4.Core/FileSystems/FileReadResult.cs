namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

public abstract record FileReadResult
{
    private FileReadResult() { }

    public sealed record Success(string Content) : FileReadResult;

    public sealed record Failure(string Message) : FileReadResult;
}