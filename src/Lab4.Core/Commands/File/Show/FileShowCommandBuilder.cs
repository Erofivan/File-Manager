using Itmo.ObjectOrientedProgramming.Lab4.Core.OutputWriters;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Show;

public sealed class FileShowCommandBuilder : ICommandBuilder
{
    private string? _path;
    private IOutputWriter? _outputWriter;

    public FileShowCommandBuilder WithPath(string path)
    {
        _path = path;
        return this;
    }

    public FileShowCommandBuilder WithOutputWriter(IOutputWriter writer)
    {
        _outputWriter = writer;
        return this;
    }

    public ICommand Build()
    {
        return new FileShowCommand(
            _path ?? throw new ArgumentNullException(nameof(_path)),
            _outputWriter ?? throw new ArgumentNullException(nameof(_outputWriter)));
    }
}