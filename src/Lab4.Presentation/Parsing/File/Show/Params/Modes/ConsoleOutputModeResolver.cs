using Itmo.ObjectOrientedProgramming.Lab4.Core.OutputWriters;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Show.Params.Modes;

public sealed class ConsoleOutputModeResolver : OutputModeResolverBase
{
    public override IOutputWriter? Resolve(string mode)
    {
        if (mode is "console")
            return new ConsoleOutputWriter();

        return CallNext(mode);
    }
}
