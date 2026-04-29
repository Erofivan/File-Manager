using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Connect;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Connect.Params.Modes;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.CommandFactories;

public sealed class ConnectCommandFactory : ICommandChainFactory
{
    public ICommandLink Create()
    {
        return new ConnectCommandLink(new ConnectModeParamHandler(new LocalFileSystemModeResolver()));
    }
}