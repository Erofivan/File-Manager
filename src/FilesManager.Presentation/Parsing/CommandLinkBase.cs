namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing;

public abstract class CommandLinkBase : ICommandLink
{
    private ICommandLink? _next;

    public abstract CommandParseResult Handle(IEnumerator<string> tokensEnumerator);

    public ICommandLink AddNext(ICommandLink link)
    {
        if (_next is null)
            _next = link;
        else
            _next.AddNext(link);

        return this;
    }

    protected CommandParseResult CallNext(IEnumerator<string> tokensEnumerator)
    {
        return _next?.Handle(tokensEnumerator) ?? new CommandParseResult.Failure("Unknown command");
    }
}