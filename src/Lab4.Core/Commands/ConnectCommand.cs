using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public sealed class ConnectCommand : ICommand
{
    private readonly string _address;
    private readonly IFileSystemFactory _fileSystemFactory;

    public ConnectCommand(string address, IFileSystemFactory fileSystemFactory)
    {
        _address = address;
        _fileSystemFactory = fileSystemFactory;
    }

    public CommandResult Execute(ExecutionContext context)
    {
        if (!_address.StartsWith('/'))
            return new CommandResult.Failure("Address must be an absolute path");

        IFileSystem fileSystem = _fileSystemFactory.Create();

        if (fileSystem.DirectoryExists(_address) is false)
            return new CommandResult.DirectoryNotFound(_address);

        context.Connect(fileSystem, _address.TrimEnd('/'));

        return new CommandResult.Success();
    }
}