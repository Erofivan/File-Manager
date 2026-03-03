using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Tree.List;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree.List.Params;

public interface ITreeListParamHandler
{
    ITreeListParamHandler AddNext(ITreeListParamHandler handler);

    void Handle(IEnumerable<string> tokens, TreeListCommandBuilder builder);
}
