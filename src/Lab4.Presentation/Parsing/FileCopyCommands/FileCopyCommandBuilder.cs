using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.FileCopyCommands;

public sealed class FileCopyCommandBuilder
{
    private string? _currentFilePath;
    private string? _newFilePath;

    public FileCopyCommandBuilder WithCurrentFilePath(string path)
    {
        _currentFilePath = path;
        return this;
    }

    public FileCopyCommandBuilder WithNewFilePath(string path)
    {
        _newFilePath = path;
        return this;
    }

    public ICommand Build()
    {
        return new FileCopyCommand(
            _currentFilePath ?? throw new ArgumentNullException(nameof(_currentFilePath)),
            _newFilePath ?? throw new ArgumentNullException(nameof(_newFilePath)));
    }
}