namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Rename;

public sealed class FileRenameCommandBuilder : ICommandBuilder
{
    private string? _filePath;
    private string? _newFileName;

    public FileRenameCommandBuilder WithFilePath(string path)
    {
        _filePath = path;
        return this;
    }

    public FileRenameCommandBuilder WithNewFileName(string name)
    {
        _newFileName = name;
        return this;
    }

    public ICommand Build()
    {
        return new FileRenameCommand(
            _filePath ?? throw new ArgumentNullException(nameof(_filePath)),
            _newFileName ?? throw new ArgumentNullException(nameof(_newFileName)));
    }
}