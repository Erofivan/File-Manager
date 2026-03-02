using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing;

public abstract class CommandLinkBase : ICommandLink
{
    private ICommandLink? _next;

    public ICommandLink AddNext(ICommandLink link)
    {
        if (_next is null)
        {
            _next = link;
        }
        else
        {
            _next.AddNext(link);
        }

        return this;
    }

    public ICommand? CallNext(string[] args)
    {
        return _next?.Handle(args);
    }

    public abstract ICommand? Handle(string[] args);

    protected static string? FindFlag(string[] args, string flagName)
    {
        for (int i = 0; i < args.Length - 1; ++i)
        {
            if (args[i].Equals(flagName, StringComparison.Ordinal))
                return args[i + 1];
        }

        return null;
    }
}