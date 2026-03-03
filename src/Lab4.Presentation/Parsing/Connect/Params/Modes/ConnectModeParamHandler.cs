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

    protected override void Apply(IEnumerable<string> tokens, ConnectCommandBuilder builder)
    {
        string? modeValue = FindFlagValue(tokens, "-m");

        if (modeValue is null)
            return;

        IFileSystemFactory? factory = _modeResolver.Resolve(modeValue);

        if (factory is not null)
            builder.WithFileSystemFactory(factory);
    }
}