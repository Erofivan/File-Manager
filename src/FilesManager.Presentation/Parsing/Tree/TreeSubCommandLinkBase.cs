namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree;

public abstract class TreeSubCommandLinkBase : ITreeSubCommandLink
{
    private ITreeSubCommandLink? _next;

    public abstract CommandParseResult Handle(IEnumerator<string> tokensEnumerator);

    public ITreeSubCommandLink AddNext(ITreeSubCommandLink link)
    {
        if (_next is null)
            _next = link;
        else
            _next.AddNext(link);

        return this;
    }

    protected CommandParseResult CallNext(IEnumerator<string> tokensEnumerator)
    {
        return _next?.Handle(tokensEnumerator) ?? new CommandParseResult.Failure("Unknown tree subcommand");
    }
}