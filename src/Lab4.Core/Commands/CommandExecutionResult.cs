namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public abstract record CommandExecutionResult
{
    private CommandExecutionResult() { }

    public sealed record Success : CommandExecutionResult;

    public sealed record FileSystemNotConnected : CommandExecutionResult;

    public sealed record FileNotFound(string Path) : CommandExecutionResult;

    public sealed record DirectoryNotFound(string Path) : CommandExecutionResult;

    public sealed record Failure(string Message) : CommandExecutionResult;
}