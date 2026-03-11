using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Tree.List;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree.List.Params;

public abstract class TreeListParamHandlerBase : ITreeListParamLink
{
    private ITreeListParamLink? _next;

    public abstract CommandParseResult Handle(IEnumerator<string> tokensEnumerator, TreeListCommandBuilder builder);

    public ITreeListParamLink AddNext(ITreeListParamLink link)
    {
        if (_next is null)
            _next = link;
        else
            _next.AddNext(link);

        return this;
    }

    protected CommandParseResult CallNext(IEnumerator<string> tokensEnumerator, TreeListCommandBuilder builder)
    {
        return _next?.Handle(tokensEnumerator, builder)
               ?? new CommandParseResult.Failure("Param handler for tree list command is missing");
    }
}
