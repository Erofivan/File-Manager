using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Connect;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Connect.Params;

public interface IConnectParamHandler
{
    IConnectParamHandler AddNext(IConnectParamHandler handler);

    void Handle(IEnumerable<string> tokens, ConnectCommandBuilder builder);
}
