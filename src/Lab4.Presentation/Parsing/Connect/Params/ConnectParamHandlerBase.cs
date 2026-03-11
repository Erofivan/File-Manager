using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Connect;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Connect.Params;

public abstract class ConnectParamHandlerBase : IConnectParamLink
{
    private IConnectParamLink? _next;

    public abstract CommandParseResult Handle(IEnumerator<string> tokensEnumerator, ConnectCommandBuilder builder);

    public IConnectParamLink AddNext(IConnectParamLink link)
    {
        if (_next is null)
            _next = link;
        else
            _next.AddNext(link);

        return this;
    }

    protected CommandParseResult CallNext(IEnumerator<string> tokensEnumerator, ConnectCommandBuilder builder)
    {
        return _next?.Handle(tokensEnumerator, builder)
               ?? new CommandParseResult.Failure("Param handler for connect command is missing");
    }
}