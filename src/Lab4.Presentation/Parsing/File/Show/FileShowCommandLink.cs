using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Show;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Show.Params;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Show;

public sealed class FileShowCommandLink : FileSubCommandLinkBase
{
    private readonly IFileShowParamHandler _flagHandler;

    public FileShowCommandLink(IFileShowParamHandler flagHandler)
    {
        _flagHandler = flagHandler;
    }

    public override CommandParseResult Handle(IEnumerator<string> tokens)
    {
        if (tokens.Current is not "show")
            return CallNext(tokens);

        if (!tokens.MoveNext())
            return new CommandParseResult.Failure("Missing path for 'file show' command");

        string path = tokens.Current;

        FileShowCommandBuilder builder = new FileShowCommandBuilder()
            .WithPath(path);

        var remaining = new List<string>();
        while (tokens.MoveNext())
            remaining.Add(tokens.Current);

        _flagHandler.Handle(remaining, builder);

        return new CommandParseResult.Success(builder.Build());
    }
}
