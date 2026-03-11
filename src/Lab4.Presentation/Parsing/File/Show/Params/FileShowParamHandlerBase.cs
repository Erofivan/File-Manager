using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Show;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Show.Params;

public abstract class FileShowParamHandlerBase : IFileShowParamLink
{
    private IFileShowParamLink? _next;

    public abstract CommandParseResult Handle(IEnumerator<string> tokensEnumerator, FileShowCommandBuilder builder);

    public IFileShowParamLink AddNext(IFileShowParamLink link)
    {
        if (_next is null)
            _next = link;
        else
            _next.AddNext(link);

        return this;
    }

    protected CommandParseResult CallNext(IEnumerator<string> tokensEnumerator, FileShowCommandBuilder builder)
    {
        return _next?.Handle(tokensEnumerator, builder)
               ?? new CommandParseResult.Failure("Param handler for file show command is missing");
    }
}
