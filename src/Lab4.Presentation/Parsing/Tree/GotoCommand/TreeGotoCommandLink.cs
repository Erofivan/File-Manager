using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Tree.GotoCommand;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree.GotoCommand;

public sealed class TreeGotoCommandLink : TreeSubCommandLinkBase
{
    public override CommandParseResult Handle(IEnumerator<string> tokens)
    {
        if (tokens.Current is not "goto")
            return CallNext(tokens);

        if (!tokens.MoveNext())
            return new CommandParseResult.Failure("Missing path for 'tree goto' command");

        string path = tokens.Current;

        TreeGotoCommandBuilder builder = new TreeGotoCommandBuilder()
            .WithPath(path);

        return new CommandParseResult.Success(builder.Build());
    }
}
