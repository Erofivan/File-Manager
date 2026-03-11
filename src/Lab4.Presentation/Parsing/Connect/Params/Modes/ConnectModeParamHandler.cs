using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Connect;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Connect.Params.Modes;

public sealed class ConnectModeParamHandler : ConnectParamHandlerBase
{
    private readonly IFileSystemModeResolver _modeResolver;

    public ConnectModeParamHandler(IFileSystemModeResolver modeResolver)
    {
        _modeResolver = modeResolver;
    }

    public override CommandParseResult Handle(IEnumerator<string> tokensEnumerator, ConnectCommandBuilder builder)
    {
        if (tokensEnumerator.Current is not "-m")
            return CallNext(tokensEnumerator, builder);

        if (tokensEnumerator.MoveNext() is false)
            return new CommandParseResult.Failure("-m flag missing value");

        IFileSystemFactory? factory = _modeResolver.Resolve(tokensEnumerator.Current);

        if (factory is not null)
            builder.WithFileSystemFactory(factory);

        return new CommandParseResult.Success(builder.Build());
    }
}