using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Disconnect;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Disconnect;

public sealed class DisconnectCommandLink : CommandLinkBase
{
    public override CommandParseResult Handle(IEnumerator<string> tokens)
    {
        if (tokens.Current is not "disconnect")
            return CallNext(tokens);

        return new CommandParseResult.Success(new DisconnectCommand());
    }
}