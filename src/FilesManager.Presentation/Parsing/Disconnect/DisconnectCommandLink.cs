using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Disconnect;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Disconnect;

public sealed class DisconnectCommandLink : CommandLinkBase
{
    public override CommandParseResult Handle(IEnumerator<string> tokensEnumerator)
    {
        if (tokensEnumerator.Current is not "disconnect")
            return CallNext(tokensEnumerator);

        return new CommandParseResult.Success(new DisconnectCommand());
    }
}