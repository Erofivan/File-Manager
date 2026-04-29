using Itmo.ObjectOrientedProgramming.Lab4.Core.OutputWriters;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Show.Params.Modes;

public interface IOutputModeResolver
{
    IOutputWriter? Resolve(string mode);
}
