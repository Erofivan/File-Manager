namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File;

public abstract class FileSubCommandLinkBase : IFileSubCommandLink
{
    private IFileSubCommandLink? _next;

    public IFileSubCommandLink AddNext(IFileSubCommandLink link)
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
            return new CommandParseResult.Failure("Unknown file subcommand");

        return _next.Handle(tokens);
    }
}
