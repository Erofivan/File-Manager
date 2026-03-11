using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.FileSystemDisplayers;
using Itmo.ObjectOrientedProgramming.Lab4.Core.OutputWriters;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation;

public sealed record CommandHandlerSettings(
    IOutputWriter OutputWriter,
    FileSystemTreeDisplaySettings TreeDisplaySettings);