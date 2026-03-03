using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Connect;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Connect.Params;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Connect;

public sealed class ConnectCommandLink : CommandLinkBase
{
    private readonly IConnectParamHandler? _flagHandler;

    public ConnectCommandLink(IConnectParamHandler? flagHandler = null)
    {
        _flagHandler = flagHandler;
    }

    public override CommandParseResult Handle(IEnumerator<string> tokens)
    {
        if (tokens.Current is not "connect")
            return CallNext(tokens);

        if (!tokens.MoveNext())
            return new CommandParseResult.Failure("Missing address for 'connect' command");

        string address = tokens.Current;

        ConnectCommandBuilder builder = new ConnectCommandBuilder()
            .WithAddress(address);

        var remaining = new List<string>();
        while (tokens.MoveNext())
            remaining.Add(tokens.Current);

        _flagHandler?.Handle(remaining, builder);

        return new CommandParseResult.Success(builder.Build());
    }
}