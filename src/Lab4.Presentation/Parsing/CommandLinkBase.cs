namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing;

public abstract class CommandLinkBase : ICommandLink
{
    private ICommandLink? _next;

    public ICommandLink AddNext(ICommandLink link)
    {
        if (_next is null)
            _next = link;
        else
            _next.AddNext(link);

        return this;
    }

    public abstract CommandParseResult Handle(IEnumerator<string> tokens);

    protected CommandParseResult CallNext(IEnumerator<string> tokens)
    {
        if (_next is null)
            return new CommandParseResult.Failure("Unknown command");

        return _next.Handle(tokens);
    }
}