using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.CommandFactories;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Disconnect;

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
        var connectCommandFactory = new ConnectCommandFactory();
        var treeCommandFactory = new TreeCommandsFactory(_settings.OutputWriter, _settings.TreeDisplaySettings);
        var fileCommandFactory = new FileCommandsFactory();

        ICommandLink connectCommandChain = connectCommandFactory.Create();
        ICommandLink treeCommandChain = treeCommandFactory.Create();
        ICommandLink fileCommandChain = fileCommandFactory.Create();

        return new CommandHandlerAdapter(
            connectCommandChain
                .AddNext(new DisconnectCommandLink())
                .AddNext(treeCommandChain)
                .AddNext(fileCommandChain));
    }

    private sealed class CommandHandlerAdapter : ICommandHandler
    {
        private readonly ICommandLink _chain;

        public CommandHandlerAdapter(ICommandLink chain)
        {
            _chain = chain;
        }

        public CommandParseResult Handle(IEnumerator<string> tokensEnumerator)
        {
            if (tokensEnumerator.MoveNext() is false)
                return new CommandParseResult.Failure("Empty command");

            return _chain.Handle(tokensEnumerator);
        }
    }
}