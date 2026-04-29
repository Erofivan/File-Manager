namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Delete;

public sealed class FileDeleteCommandBuilder : ICommandBuilder
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