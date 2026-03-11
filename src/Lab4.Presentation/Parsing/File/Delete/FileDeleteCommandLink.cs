using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Delete;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Delete;

public sealed class FileDeleteCommandLink : FileSubCommandLinkBase
{
    public override CommandParseResult Handle(IEnumerator<string> tokensEnumerator)
    {
        if (tokensEnumerator.Current is not "delete")
            return CallNext(tokensEnumerator);

        if (tokensEnumerator.MoveNext() is false)
            return new CommandParseResult.Failure("Missing path for 'file delete' command");

        FileDeleteCommandBuilder builder = new FileDeleteCommandBuilder()
            .WithPath(tokensEnumerator.Current);

        return new CommandParseResult.Success(builder.Build());
    }
}