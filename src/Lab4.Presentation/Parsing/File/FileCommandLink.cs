namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File;

public sealed class FileCommandLink : CommandLinkBase
{
    private readonly IFileSubCommandLink _subChain;

    public FileCommandLink(IFileSubCommandLink subChain)
    {
        _subChain = subChain;
    }

    public override CommandParseResult Handle(IEnumerator<string> tokens)
    {
        if (tokens.Current is not "file")
            return CallNext(tokens);

        if (!tokens.MoveNext())
            return new CommandParseResult.Failure("Missing subcommand after 'file'");

        return _subChain.Handle(tokens);
    }
}
