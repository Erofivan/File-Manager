using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.TreeListCommands;

public sealed class TreeListCommandBuilder
{
    private const int DefaultDepth = 1;

    private int _depth = DefaultDepth;

    public TreeListCommandBuilder WithDepth(int depth)
    {
        _depth = depth;
        return this;
    }

    public ICommand Build()
    {
        return new TreeListCommand(_depth);
    }
}