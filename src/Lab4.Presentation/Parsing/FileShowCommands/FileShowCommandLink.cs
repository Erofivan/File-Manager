using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Core.OutputWriters;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.FileShowCommands.ModeResolvers;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.FileShowCommands;

public sealed class FileShowCommandLink : CommandLinkBase
{
    private const string DefaultMode = "console";

    private readonly IOutputModeResolver _modeResolver;

    public FileShowCommandLink(IOutputModeResolver modeResolver)
    {
        _modeResolver = modeResolver;
    }

    public override ICommand? Handle(string[] args)
    {
        if (args.Length < 3 || args[0] is not "file" || args[1] is not "show")
            return CallNext(args);

        string mode = FindFlag(args, "-m") ?? DefaultMode;

        IOutputWriter? writer = _modeResolver.Resolve(mode);

        if (writer is null)
            return CallNext(args);

        FileShowCommandBuilder builder = new FileShowCommandBuilder()
            .WithPath(args[2])
            .WithOutputWriter(writer);

        return builder.Build();
    }
}