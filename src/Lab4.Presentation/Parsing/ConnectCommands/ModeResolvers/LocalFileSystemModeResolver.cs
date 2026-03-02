using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.ConnectCommands.ModeResolvers;

public sealed class LocalFileSystemModeResolver : FileSystemModeLinkBase
{
    public override IFileSystemFactory? Resolve(string mode)
    {
        if (mode is "local")
            return new LocalFileSystemFactory();

        return CallNext(mode);
    }
}