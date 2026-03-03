namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree;

public sealed class TreeCommandLink : CommandLinkBase
{
    private readonly ITreeSubCommandLink _subChain;

    public TreeCommandLink(ITreeSubCommandLink subChain)
    {
        _subChain = subChain;
    }

    public override CommandParseResult Handle(IEnumerator<string> tokens)
    {
        if (tokens.Current is not "tree")
            return CallNext(tokens);

        if (!tokens.MoveNext())
            return new CommandParseResult.Failure("Missing subcommand after 'tree'");

        return _subChain.Handle(tokens);
    }
}
