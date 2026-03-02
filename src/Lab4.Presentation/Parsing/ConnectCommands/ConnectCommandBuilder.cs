using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.ConnectCommands;

public sealed class ConnectCommandBuilder
{
    private string? _address;
    private IFileSystemFactory? _fileSystemFactory;

    public ConnectCommandBuilder WithAddress(string address)
    {
        _address = address;
        return this;
    }

    public ConnectCommandBuilder WithFileSystemFactory(IFileSystemFactory factory)
    {
        _fileSystemFactory = factory;
        return this;
    }

    public ICommand Build()
    {
        return new ConnectCommand(
            _address ?? throw new ArgumentNullException(nameof(_address)),
            _fileSystemFactory ?? throw new ArgumentNullException(nameof(_fileSystemFactory)));
    }
}