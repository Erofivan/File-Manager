namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

public sealed class TreeGotoCommand : ICommand
{
    private readonly string _path;

    public TreeGotoCommand(string path)
    {
        _path = path;
    }

    public CommandResult Execute(ExecutionContext context)
    {
        if (context.FileSystem.IsConnected is false)
            return new CommandResult.FileSystemNotConnected();

        string resolvedPath = context.ResolvePath(_path);

        if (context.FileSystem.DirectoryExists(resolvedPath) is false)
            return new CommandResult.DirectoryNotFound(_path);

        string localPath = resolvedPath.Substring(context.ConnectionPath.Length);
        if (string.IsNullOrEmpty(localPath))
            localPath = "/";

        context.SetCurrentPath(localPath);

        return new CommandResult.Success();
    }
}