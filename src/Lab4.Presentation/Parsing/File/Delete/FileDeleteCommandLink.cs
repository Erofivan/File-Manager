using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Delete;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Delete;

public sealed class FileDeleteCommandLink : FileSubCommandLinkBase
{
    public override CommandParseResult Handle(IEnumerator<string> tokens)
    {
        if (tokens.Current is not "delete")
            return CallNext(tokens);

        if (!tokens.MoveNext())
            return new CommandParseResult.Failure("Missing path for 'file delete' command");

        string path = tokens.Current;

        FileDeleteCommandBuilder builder = new FileDeleteCommandBuilder()
            .WithPath(path);

        return new CommandParseResult.Success(builder.Build());
    }
}