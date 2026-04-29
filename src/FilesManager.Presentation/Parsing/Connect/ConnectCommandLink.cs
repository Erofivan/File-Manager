using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Connect;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Connect.Params;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Connect;

public sealed class ConnectCommandLink : CommandLinkBase
{
    private readonly IConnectParamHandler _paramHandler;

    public ConnectCommandLink(IConnectParamHandler paramHandler)
    {
        _paramHandler = paramHandler;
    }

    public override CommandParseResult Handle(IEnumerator<string> tokensEnumerator)
    {
        if (tokensEnumerator.Current is not "connect")
            return CallNext(tokensEnumerator);

        if (tokensEnumerator.MoveNext() is false)
            return new CommandParseResult.Failure("Missing address for 'connect' command");

        ConnectCommandBuilder builder = new ConnectCommandBuilder()
            .WithAddress(tokensEnumerator.Current);

        if (tokensEnumerator.MoveNext() is false)
            return new CommandParseResult.Success(builder.Build());

        CommandParseResult result = _paramHandler.Handle(tokensEnumerator, builder);

        if (result is CommandParseResult.Failure)
            return result;

        return new CommandParseResult.Success(builder.Build());
    }
}