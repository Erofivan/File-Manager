using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Connect.Params.Modes;

public abstract class FileSystemModeResolverBase : IFileSystemModeResolver
{
    private FileSystemModeResolverBase? _next;

    public abstract IFileSystemFactory? Resolve(string mode);

    public FileSystemModeResolverBase AddNext(FileSystemModeResolverBase resolver)
    {
        if (_next is null)
            _next = resolver;
        else
            _next.AddNext(resolver);

        return this;
    }

    protected IFileSystemFactory? CallNext(string mode)
    {
        return _next?.Resolve(mode);
    }
}