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

    public override CommandParseResult Handle(IEnumerator<string> tokensEnumerator)
    {
        if (tokensEnumerator.Current is not "show")
            return CallNext(tokensEnumerator);

        if (tokensEnumerator.MoveNext() is false)
            return new CommandParseResult.Failure("Missing path for 'file show' command");

        FileShowCommandBuilder builder = new FileShowCommandBuilder()
            .WithPath(tokensEnumerator.Current);

        if (tokensEnumerator.MoveNext() is false)
            return new CommandParseResult.Failure("-m flag is required");

        CommandParseResult result = _flagHandler.Handle(tokensEnumerator, builder);

        if (result is CommandParseResult.Failure)
            return result;

        return new CommandParseResult.Success(builder.Build());
    }
}
