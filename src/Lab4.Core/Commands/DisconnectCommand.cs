namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public sealed class DisconnectCommand : ICommand
{
    public CommandResult Execute(ExecutionContext context)
    {
        if (context.FileSystem.IsConnected is false)
            return new CommandResult.FileSystemNotConnected();

        context.Disconnect();

        return new CommandResult.Success();
    }
}