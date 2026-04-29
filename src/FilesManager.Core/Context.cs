using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core;

public sealed class Context
{
    public Context()
    {
        FileSystem = new NullFileSystem();
        ConnectionPath = string.Empty;
        CurrentPath = string.Empty;
    }

    public IFileSystem FileSystem { get; private set; }

    public string ConnectionPath { get; private set; }

    public string CurrentPath { get; private set; }

    public string CurrentFullPath
    {
        get
        {
            if (string.IsNullOrEmpty(CurrentPath))
                return ConnectionPath;

            return FileSystem.CombinePaths(ConnectionPath, CurrentPath);
        }
    }

    public bool IsConnected => FileSystem.IsConnected;

    public void Connect(IFileSystem fileSystem, string connectionPath)
    {
        FileSystem = fileSystem;
        ConnectionPath = fileSystem.NormalizePath(connectionPath);
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
        string combined;

        if (FileSystem.IsAbsolutePath(path))
        {
            combined = FileSystem.CombinePaths(ConnectionPath, path);
        }
        else
        {
            combined = FileSystem.CombinePaths(CurrentFullPath, path);
        }

        string normalized = FileSystem.NormalizePath(combined);

        return ClampToConnectionPath(normalized);
    }

    private string ClampToConnectionPath(string path)
    {
        return FileSystem.IsPathWithinBasePath(path, ConnectionPath) ? path : ConnectionPath;
    }
}