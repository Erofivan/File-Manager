using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Components;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

public sealed class NullFileSystem : IFileSystem
{
    public bool IsConnected => false;

    public IEnumerable<IFileSystemComponent> ListDirectory(string path, int depth)
    {
        return [];
    }

    public FileReadResult ReadFile(string path)
    {
        return new FileReadResult.Failure("File system is not connected");
    }

    public FileOperationResult MoveFile(string path)
    {
        return new FileOperationResult.Failure("File system is not connected");
    }

    public FileOperationResult CopyFile(string oldPath, string newPath)
    {
        return new FileOperationResult.Failure("File system is not connected");
    }

    public FileOperationResult DeleteFile(string path)
    {
        return new FileOperationResult.Failure("File system is not connected");
    }

    public FileOperationResult RenameFile(string path, string newName)
    {
        return new FileOperationResult.Failure("File system is not connected");
    }

    public bool FileExists(string path)
    {
        return false;
    }

    public bool DirectoryExists(string path)
    {
        return false;
    }
}