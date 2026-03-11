using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Show;
using Itmo.ObjectOrientedProgramming.Lab4.Core.OutputWriters;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Show.Params.Modes;

public sealed class FileShowModeParamHandler : FileShowParamHandlerBase
{
    private readonly IOutputModeResolver _modeResolver;

    public FileShowModeParamHandler(IOutputModeResolver modeResolver)
    {
        _modeResolver = modeResolver;
    }

    public override CommandParseResult Handle(IEnumerator<string> tokensEnumerator, FileShowCommandBuilder builder)
    {
        if (tokensEnumerator.Current is not "-m")
            return CallNext(tokensEnumerator, builder);

        if (tokensEnumerator.MoveNext() is false)
            return new CommandParseResult.Failure("-m flag missing value");

        IOutputWriter? writer = _modeResolver.Resolve(tokensEnumerator.Current);

        if (writer is not null)
            builder.WithOutputWriter(writer);

        return new CommandParseResult.Success(builder.Build());
    }
}
