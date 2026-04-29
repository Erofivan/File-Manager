using Itmo.ObjectOrientedProgramming.Lab4.Core.OutputWriters;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Show.Params.Modes;

public abstract class OutputModeResolverBase : IOutputModeResolver
{
    private OutputModeResolverBase? _next;

    public abstract IOutputWriter? Resolve(string mode);

    public OutputModeResolverBase AddNext(OutputModeResolverBase link)
    {
        if (_next is null)
            _next = link;
        else
            _next.AddNext(link);

        return this;
    }

    protected IOutputWriter? CallNext(string mode)
    {
        return _next?.Resolve(mode);
    }
}
