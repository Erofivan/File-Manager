namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Tree.GotoCommand;

public sealed class TreeGotoCommand : ICommand
{
    private readonly string _path;

    public TreeGotoCommand(string path)
    {
        _path = path;
    }

    public CommandExecutionResult Execute(Context context)
    {
        if (context.IsConnected is false)
            return new CommandExecutionResult.FileSystemNotConnected();

        string resolvedPath = context.ResolvePath(_path);

        if (context.FileSystem.DirectoryExists(resolvedPath) is false)
            return new CommandExecutionResult.DirectoryNotFound(_path);

        string localPath = resolvedPath[context.ConnectionPath.Length..];

        context.SetCurrentPath(localPath);

        return new CommandExecutionResult.Success();
    }
}