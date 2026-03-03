using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Show;
using Itmo.ObjectOrientedProgramming.Lab4.Core.OutputWriters;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Show.Params.Modes;

public sealed class FileShowModeParamHandler : FileShowParamHandlerBase
{
    private const string DefaultMode = "console";

    private readonly IOutputModeResolver _modeResolver;

    public FileShowModeParamHandler(IOutputModeResolver modeResolver)
    {
        _modeResolver = modeResolver;
    }

    protected override void Apply(IEnumerable<string> tokens, FileShowCommandBuilder builder)
    {
        string mode = FindFlagValue(tokens, "-m") ?? DefaultMode;

        IOutputWriter? writer = _modeResolver.Resolve(mode);

        if (writer is not null)
            builder.WithOutputWriter(writer);
    }
}
