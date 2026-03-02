using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.TreeGotoCommands;

public sealed class TreeGotoCommandBuilder
{
    private string? _path;

    public TreeGotoCommandBuilder WithPath(string path)
    {
        _path = path;
        return this;
    }

    public ICommand Build()
    {
        return new TreeGotoCommand(
            _path ?? throw new ArgumentNullException(_path));
    }
}