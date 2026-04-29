namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree;

public sealed class TreeCommandLink : CommandLinkBase
{
    private readonly ITreeSubCommandLink _subChain;

    public TreeCommandLink(ITreeSubCommandLink subChain)
    {
        _subChain = subChain;
    }

    public override CommandParseResult Handle(IEnumerator<string> tokensEnumerator)
    {
        if (tokensEnumerator.Current is not "tree")
            return CallNext(tokensEnumerator);

        if (tokensEnumerator.MoveNext() is false)
            return new CommandParseResult.Failure("Missing subcommand after 'tree'");

        return _subChain.Handle(tokensEnumerator);
    }
}