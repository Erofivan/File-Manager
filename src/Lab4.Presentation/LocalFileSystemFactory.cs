using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation;

public sealed class LocalFileSystemFactory : IFileSystemFactory
{
    public IFileSystem Create()
    {
        return new LocalFileSystem();
    }
}