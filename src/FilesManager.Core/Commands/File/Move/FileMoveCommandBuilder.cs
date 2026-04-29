namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Move;

public sealed class FileMoveCommandBuilder : ICommandBuilder
{
    private string? _currentFilePath;
    private string? _newFilePath;

    public FileMoveCommandBuilder WithCurrentFilePath(string path)
    {
        _currentFilePath = path;
        return this;
    }

    public FileMoveCommandBuilder WithNewFilePath(string path)
    {
        _newFilePath = path;
        return this;
    }

    public ICommand Build()
    {
        return new FileMoveCommand(
            _currentFilePath ?? throw new ArgumentNullException(nameof(_currentFilePath)),
            _newFilePath ?? throw new ArgumentNullException(nameof(_newFilePath)));
    }
}