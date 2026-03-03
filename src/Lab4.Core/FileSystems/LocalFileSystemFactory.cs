namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

public sealed class LocalFileSystemFactory : IFileSystemFactory
{
    public IFileSystem Create()
    {
        return new LocalFileSystem();
    }
}