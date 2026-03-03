using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Connect;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Connect.Params.Modes;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Disconnect;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Copy;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Delete;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Move;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Rename;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Show;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Show.Params.Modes;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree.GotoCommand;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree.List;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree.List.Params.Depths;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing;

public sealed class CommandHandlerFactory
{
    private readonly CommandHandlerSettings _settings;

    public CommandHandlerFactory(CommandHandlerSettings settings)
    {
        _settings = settings;
    }

    public ICommandHandler Create()
    {
        return new CommandHandlerAdapter(
            new ConnectCommandLink(
                    new ConnectModeParamHandler(
                        new LocalFileSystemModeResolver()))
                .AddNext(new DisconnectCommandLink())
                .AddNext(new TreeCommandLink(
                    new TreeGotoCommandLink()
                        .AddNext(new TreeListCommandLink(
                            _settings.OutputWriter,
                            _settings.TreeDisplaySettings,
                            new DepthParamHandler()))))
                .AddNext(new FileCommandLink(
                    new FileShowCommandLink(
                            new FileShowModeParamHandler(
                                new ConsoleOutputModeResolver()))
                        .AddNext(new FileMoveCommandLink())
                        .AddNext(new FileCopyCommandLink())
                        .AddNext(new FileDeleteCommandLink())
                        .AddNext(new FileRenameCommandLink()))));
    }

    private sealed class CommandHandlerAdapter : ICommandHandler
    {
        private readonly ICommandLink _chain;

        public CommandHandlerAdapter(ICommandLink chain)
        {
            _chain = chain;
        }

        public CommandParseResult Handle(IEnumerable<string> tokens)
        {
            using IEnumerator<string> enumerator = tokens.GetEnumerator();

            if (enumerator.MoveNext() is false)
                return new CommandParseResult.Failure("Empty command");

            return _chain.Handle(enumerator);
        }
    }
}