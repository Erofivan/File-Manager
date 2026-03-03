using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Connect;

public sealed class ConnectCommandBuilder : ICommandBuilder
{
    private static readonly IFileSystemFactory DefaultFileSystemFactory = new LocalFileSystemFactory();

    private string? _address;
    private IFileSystemFactory _fileSystemFactory = DefaultFileSystemFactory;

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
            _fileSystemFactory);
    }
}