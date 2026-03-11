namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File;

public sealed class FileCommandLink : CommandLinkBase
{
    private readonly IFileSubCommandLink _subChain;

    public FileCommandLink(IFileSubCommandLink subChain)
    {
        _subChain = subChain;
    }

    public override CommandParseResult Handle(IEnumerator<string> tokensEnumerator)
    {
        if (tokensEnumerator.Current is not "file")
            return CallNext(tokensEnumerator);

        if (tokensEnumerator.MoveNext() is false)
            return new CommandParseResult.Failure("Missing subcommand after 'file'");

        return _subChain.Handle(tokensEnumerator);
    }
}
