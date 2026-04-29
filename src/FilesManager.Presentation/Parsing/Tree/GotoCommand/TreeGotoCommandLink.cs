using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Tree.GotoCommand;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree.GotoCommand;

public sealed class TreeGotoCommandLink : TreeSubCommandLinkBase
{
    public override CommandParseResult Handle(IEnumerator<string> tokensEnumerator)
    {
        if (tokensEnumerator.Current is not "goto")
            return CallNext(tokensEnumerator);

        if (tokensEnumerator.MoveNext() is false)
            return new CommandParseResult.Failure("Missing path for 'tree goto' command");

        TreeGotoCommandBuilder builder = new TreeGotoCommandBuilder()
            .WithPath(tokensEnumerator.Current);

        return new CommandParseResult.Success(builder.Build());
    }
}
