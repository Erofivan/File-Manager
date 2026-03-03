using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Tree.List;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree.List.Params;

public abstract class TreeListParamHandlerBase : ITreeListParamHandler
{
    private ITreeListParamHandler? _next;

    public ITreeListParamHandler AddNext(ITreeListParamHandler handler)
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

    public void Handle(IEnumerable<string> tokens, TreeListCommandBuilder builder)
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

    protected abstract void Apply(IEnumerable<string> tokens, TreeListCommandBuilder builder);
}
