using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.ConnectCommands.ModeResolvers;

public abstract class FileSystemModeLinkBase : IFileSystemModeResolver
{
    private FileSystemModeLinkBase? _next;

    public abstract IFileSystemFactory? Resolve(string mode);

    public FileSystemModeLinkBase AddNext(FileSystemModeLinkBase link)
    {
        if (_next is null)
        {
            _next = link;
        }
        else
        {
            _next.AddNext(link);
        }

        return this;
    }

    protected IFileSystemFactory? CallNext(string mode)
    {
        return _next?.Resolve(mode);
    }
}