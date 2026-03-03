using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Connect.Params.Modes;

public sealed class LocalFileSystemModeResolver : FileSystemModeResolverBase
{
    public override IFileSystemFactory? Resolve(string mode)
    {
        if (mode is "local")
            return new LocalFileSystemFactory();

        return CallNext(mode);
    }
}