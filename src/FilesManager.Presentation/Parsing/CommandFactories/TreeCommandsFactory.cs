using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.FileSystemDisplayers;
using Itmo.ObjectOrientedProgramming.Lab4.Core.OutputWriters;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree.GotoCommand;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree.List;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree.List.Params.Depths;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.CommandFactories;

public sealed class TreeCommandsFactory : ICommandChainFactory
{
    private readonly IOutputWriter _outputWriter;
    private readonly FileSystemTreeDisplaySettings _treeDisplaySettings;

    public TreeCommandsFactory(IOutputWriter outputWriter, FileSystemTreeDisplaySettings treeDisplaySettings)
    {
        _outputWriter = outputWriter;
        _treeDisplaySettings = treeDisplaySettings;
    }

    public ICommandLink Create()
    {
        return new TreeCommandLink(
            new TreeGotoCommandLink()
                .AddNext(new TreeListCommandLink(
                    _outputWriter,
                    _treeDisplaySettings,
                    new DepthParamHandler())));
    }
}