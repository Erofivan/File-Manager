using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.FileDeleteCommands;

public sealed class FileDeleteCommandBuilder
{
    private string? _path;

    public FileDeleteCommandBuilder WithPath(string path)
    {
        _path = path;
        return this;
    }

    public ICommand Build()
    {
        return new FileDeleteCommand(
            _path ?? throw new ArgumentNullException(nameof(_path)));
    }
}