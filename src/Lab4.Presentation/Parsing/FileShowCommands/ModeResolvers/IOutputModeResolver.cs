using Itmo.ObjectOrientedProgramming.Lab4.Core.OutputWriters;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.FileShowCommands.ModeResolvers;

public interface IOutputModeResolver
{
    IOutputWriter? Resolve(string mode);
}