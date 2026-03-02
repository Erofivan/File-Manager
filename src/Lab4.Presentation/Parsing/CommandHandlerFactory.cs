using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.ConnectCommands;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.ConnectCommands.ModeResolvers;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.DisconnectCommands;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.FileCopyCommands;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.FileDeleteCommands;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.FileMoveCommands;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.FileRenameCommands;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.FileShowCommands;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.FileShowCommands.ModeResolvers;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.TreeGotoCommands;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.TreeListCommands;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing;

public sealed class CommandHandlerFactory
{
    public ICommandHandler Create()
    {
        return new ConnectCommandLink(
                new LocalFileSystemModeResolver())
            .AddNext(new DisconnectCommandLink())
            .AddNext(new TreeGotoCommandLink())
            .AddNext(new TreeListCommandLink())
            .AddNext(new FileShowCommandLink(
                new ConsoleOutputModeResolver()))
            .AddNext(new FileMoveCommandLink())
            .AddNext(new FileCopyCommandLink())
            .AddNext(new FileDeleteCommandLink())
            .AddNext(new FileRenameCommandLink());
    }
}