using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Show;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Show.Params;

public interface IFileShowParamHandler
{
    CommandParseResult Handle(IEnumerator<string> tokensEnumerator, FileShowCommandBuilder builder);
}

public interface IFileShowParamLink : IFileShowParamHandler
{
    IFileShowParamLink AddNext(IFileShowParamLink link);
}