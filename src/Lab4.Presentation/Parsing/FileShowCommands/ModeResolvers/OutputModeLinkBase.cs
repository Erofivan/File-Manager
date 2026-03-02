using Itmo.ObjectOrientedProgramming.Lab4.Core.OutputWriters;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.FileShowCommands.ModeResolvers;

public abstract class OutputModeLinkBase : IOutputModeResolver
{
    private OutputModeLinkBase? _next;

    public abstract IOutputWriter? Resolve(string mode);

    public OutputModeLinkBase AddNext(OutputModeLinkBase link)
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

    protected IOutputWriter? CallNext(string mode)
    {
        return _next?.Resolve(mode);
    }
}