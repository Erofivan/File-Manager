using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Connect;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Connect.Params;

public interface IConnectParamHandler
{
    CommandParseResult Handle(IEnumerator<string> tokensEnumerator, ConnectCommandBuilder builder);
}

public interface IConnectParamLink : IConnectParamHandler
{
    IConnectParamLink AddNext(IConnectParamLink link);
}
