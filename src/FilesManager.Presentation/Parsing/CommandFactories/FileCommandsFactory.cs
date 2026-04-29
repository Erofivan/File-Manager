using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Copy;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Delete;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Move;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Rename;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Show;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Show.Params.Modes;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.CommandFactories;

public sealed class FileCommandsFactory : ICommandChainFactory
{
    public ICommandLink Create()
    {
        return new FileCommandLink(
            new FileShowCommandLink(new FileShowModeParamHandler(new ConsoleOutputModeResolver()))
                .AddNext(new FileMoveCommandLink())
                .AddNext(new FileCopyCommandLink())
                .AddNext(new FileDeleteCommandLink())
                .AddNext(new FileRenameCommandLink()));
    }
}