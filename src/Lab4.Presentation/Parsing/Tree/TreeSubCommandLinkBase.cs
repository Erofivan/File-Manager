namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree;

public abstract class TreeSubCommandLinkBase : ITreeSubCommandLink
{
    private ITreeSubCommandLink? _next;

    public ITreeSubCommandLink AddNext(ITreeSubCommandLink link)
    {
        if (_next is null)
        {
            _next = link;
        }
        else
        {
            _next.AddNext(link);
        }

        return this;
    }

    public abstract CommandParseResult Handle(IEnumerator<string> tokens);

    protected CommandParseResult CallNext(IEnumerator<string> tokens)
    {
        if (_next is null)
            return new CommandParseResult.Failure("Unknown tree subcommand");

        return _next.Handle(tokens);
    }
}
