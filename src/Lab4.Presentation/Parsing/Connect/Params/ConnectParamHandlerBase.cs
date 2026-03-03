using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Connect;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Connect.Params;

public abstract class ConnectParamHandlerBase : IConnectParamHandler
{
    private IConnectParamHandler? _next;

    public IConnectParamHandler AddNext(IConnectParamHandler handler)
    {
        if (_next is null)
        {
            _next = handler;
        }
        else
        {
            _next.AddNext(handler);
        }

        return this;
    }

    public void Handle(IEnumerable<string> tokens, ConnectCommandBuilder builder)
    {
        IEnumerable<string> enumerable = tokens.ToList();
        Apply(enumerable, builder);
        _next?.Handle(enumerable, builder);
    }

    protected static string? FindFlagValue(IEnumerable<string> tokens, string flagName)
    {
        using IEnumerator<string> enumerator = tokens.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (string.Equals(enumerator.Current, flagName, StringComparison.Ordinal)
                && enumerator.MoveNext())
            {
                return enumerator.Current;
            }
        }

        return null;
    }

    protected abstract void Apply(IEnumerable<string> tokens, ConnectCommandBuilder builder);
}