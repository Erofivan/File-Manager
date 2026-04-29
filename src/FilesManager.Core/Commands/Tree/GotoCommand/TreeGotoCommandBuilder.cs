namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Tree.GotoCommand;

public sealed class TreeGotoCommandBuilder : ICommandBuilder
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