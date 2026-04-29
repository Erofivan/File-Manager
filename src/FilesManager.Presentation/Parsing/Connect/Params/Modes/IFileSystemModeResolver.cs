using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Connect.Params.Modes;

public interface IFileSystemModeResolver
{
    IFileSystemFactory? Resolve(string mode);
}
