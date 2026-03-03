using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Show;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Show.Params;

public interface IFileShowParamHandler
{
    IFileShowParamHandler AddNext(IFileShowParamHandler handler);

    void Handle(IEnumerable<string> tokens, FileShowCommandBuilder builder);
}
