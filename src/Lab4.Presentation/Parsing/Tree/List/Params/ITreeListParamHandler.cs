using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Tree.List;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree.List.Params;

public interface ITreeListParamHandler
{
    CommandParseResult Handle(IEnumerator<string> tokensEnumerator, TreeListCommandBuilder builder);
}

public interface ITreeListParamLink : ITreeListParamHandler
{
    ITreeListParamLink AddNext(ITreeListParamLink link);
}
