using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Tree.List;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.FileSystemDisplayers;
using Itmo.ObjectOrientedProgramming.Lab4.Core.OutputWriters;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree.List.Params;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree.List;

public sealed class TreeListCommandLink : TreeSubCommandLinkBase
{
    private readonly IOutputWriter _outputWriter;
    private readonly FileSystemTreeDisplaySettings _treeDisplaySettings;
    private readonly ITreeListParamHandler? _flagHandler;

    public TreeListCommandLink(
        IOutputWriter outputWriter,
        FileSystemTreeDisplaySettings treeDisplaySettings,
        ITreeListParamHandler? flagHandler = null)
    {
        _outputWriter = outputWriter;
        _treeDisplaySettings = treeDisplaySettings;
        _flagHandler = flagHandler;
    }

    public override CommandParseResult Handle(IEnumerator<string> tokensEnumerator)
    {
        if (tokensEnumerator.Current is not "list")
            return CallNext(tokensEnumerator);

        TreeListCommandBuilder builder = new TreeListCommandBuilder()
            .WithOutputWriter(_outputWriter)
            .WithTreeDisplaySettings(_treeDisplaySettings);

        _flagHandler?.Handle(tokensEnumerator, builder);

        return new CommandParseResult.Success(builder.Build());
    }
}
