using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing;

public abstract record CommandParseResult
{
    private CommandParseResult() { }

    public sealed record Success(ICommand Command) : CommandParseResult;

    public sealed record Failure(string Message) : CommandParseResult;
}
