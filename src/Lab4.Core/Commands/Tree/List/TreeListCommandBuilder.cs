using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.FileSystemDisplayers;
using Itmo.ObjectOrientedProgramming.Lab4.Core.OutputWriters;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Tree.List;

public sealed class TreeListCommandBuilder : ICommandBuilder
{
    private const int DefaultDepth = 1;

    private int _depth = DefaultDepth;
    private IOutputWriter? _outputWriter;
    private FileSystemTreeDisplaySettings? _treeDisplaySettings;

    public TreeListCommandBuilder WithDepth(int depth)
    {
        _depth = depth;
        return this;
    }

    public TreeListCommandBuilder WithOutputWriter(IOutputWriter outputWriter)
    {
        _outputWriter = outputWriter;
        return this;
    }

    public TreeListCommandBuilder WithTreeDisplaySettings(FileSystemTreeDisplaySettings treeDisplaySettings)
    {
        _treeDisplaySettings = treeDisplaySettings;
        return this;
    }

    public ICommand Build()
    {
        return new TreeListCommand(
            _depth,
            _outputWriter ?? throw new ArgumentNullException(nameof(_outputWriter)),
            _treeDisplaySettings ?? throw new ArgumentNullException(nameof(_treeDisplaySettings)));
    }
}