using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Trees;
using Itmo.ObjectOrientedProgramming.Lab4.Core.OutputWriters;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core;

public sealed class ExecutionContext
{
    public ExecutionContext(IOutputWriter outputWriter, FileSystemTreeDisplaySettings treeDisplaySettings)
    {
        OutputWriter = outputWriter;
        TreeDisplaySettings = treeDisplaySettings;
        FileSystem = new NullFileSystem();
        ConnectionPath = string.Empty;
        CurrentPath = string.Empty;
    }

    public IOutputWriter OutputWriter { get; }

    public FileSystemTreeDisplaySettings TreeDisplaySettings { get; }

    public IFileSystem FileSystem { get; private set; }

    public string ConnectionPath { get; private set; }

    public string CurrentPath { get; private set; }

    public void Connect(IFileSystem fileSystem, string connectionPath)
    {
        FileSystem = fileSystem;
        ConnectionPath = connectionPath;
        CurrentPath = string.Empty;
    }

    public void Disconnect()
    {
        FileSystem = new NullFileSystem();
        ConnectionPath = string.Empty;
        CurrentPath = string.Empty;
    }

    public void SetCurrentPath(string path)
    {
        CurrentPath = path;
    }

    public string ResolvePath(string path)
    {
        if (path.StartsWith('/'))
        {
            string combined = ConnectionPath + path;

            return ClampToConnectionPath(NormalisePath(combined));
        }

        string basePath = ConnectionPath + CurrentPath;
        string fullPath = basePath.TrimEnd('/') + "/" + path;

        return ClampToConnectionPath(NormalisePath(fullPath));
    }

    private static string NormalisePath(string path)
    {
        string[] pathTokens = path.Split('/');
        var pathTokensStack = new Stack<string>();

        foreach (string token in pathTokens)
        {
            if (token is ".")
                continue;

            if (token is ".." && pathTokensStack.Count > 0)
            {
                pathTokensStack.Pop();
                continue;
            }

            if (token is "..")
                continue;

            pathTokensStack.Push(token);
        }

        var resultPathTokenList = new List<string>(pathTokensStack);
        resultPathTokenList.Reverse();

        return "/" + string.Join("/", resultPathTokenList);
    }

    private string ClampToConnectionPath(string path)
    {
        if (path.StartsWith(ConnectionPath + '/', StringComparison.Ordinal))
            return path;

        return ConnectionPath;
    }
}