namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public sealed class DisconnectCommand : ICommand
{
    public CommandExecutionResult Execute(Context context)
    {
        if (context.FileSystem.IsConnected is false)
            return new CommandExecutionResult.FileSystemNotConnected();

        context.Disconnect();

        return new CommandExecutionResult.Success();
    }
}