using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.ConnectCommands.ModeResolvers;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.ConnectCommands;

public sealed class ConnectCommandLink : CommandLinkBase
{
    private const string DefaultMode = "local";

    private readonly IFileSystemModeResolver _modeResolver;

    public ConnectCommandLink(IFileSystemModeResolver modeResolver)
    {
        _modeResolver = modeResolver;
    }

    public override ICommand? Handle(string[] args)
    {
        if (args.Length < 2 || args[0] is not "connect")
            return CallNext(args);

        string mode = FindFlag(args, "-m") ?? DefaultMode;

        IFileSystemFactory? factory = _modeResolver.Resolve(mode);

        if (factory is null)
            return CallNext(args);

        ConnectCommandBuilder builder = new ConnectCommandBuilder()
            .WithAddress(args[1])
            .WithFileSystemFactory(factory);

        return builder.Build();
    }
}