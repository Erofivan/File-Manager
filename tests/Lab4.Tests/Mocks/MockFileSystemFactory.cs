using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests.Mocks;

public sealed class MockFileSystemFactory : IFileSystemFactory
{
    public MockFileSystemFactory(IFileSystem fileSystem)
    {
        FileSystem = fileSystem;
    }

    private IFileSystem FileSystem { get; }

    public IFileSystem Create()
    {
        return FileSystem;
    }
}