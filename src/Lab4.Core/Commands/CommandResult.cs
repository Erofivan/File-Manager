namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public abstract record CommandResult
{
    private CommandResult() { }

    public sealed record Success : CommandResult;

    public sealed record FileSystemNotConnected : CommandResult;

    public sealed record FileNotFound(string Path) : CommandResult;

    public sealed record DirectoryNotFound(string Path) : CommandResult;

    public sealed record Failure(string Message) : CommandResult;
}