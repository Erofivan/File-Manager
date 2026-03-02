using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.DisconnectCommands;

public sealed class DisconnectCommandLink : CommandLinkBase
{
    public override ICommand? Handle(string[] args)
    {
        if (args.Length != 1 || args[0] is not "disconnect")
            return CallNext(args);

        return new DisconnectCommand();
    }
}