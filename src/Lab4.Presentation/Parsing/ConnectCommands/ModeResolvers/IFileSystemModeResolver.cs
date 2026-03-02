using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.ConnectCommands.ModeResolvers;

public interface IFileSystemModeResolver
{
    IFileSystemFactory? Resolve(string mode);
}
