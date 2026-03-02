using Itmo.ObjectOrientedProgramming.Lab4.Core.OutputWriters;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.FileShowCommands.ModeResolvers;

public sealed class ConsoleOutputModeResolver : OutputModeLinkBase
{
    public override IOutputWriter? Resolve(string mode)
    {
        if (mode is "console")
            return new ConsoleOutputWriter();

        return CallNext(mode);
    }
}