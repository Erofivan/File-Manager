using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Components;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

public sealed class NullFileSystem : IFileSystem
{
    public bool IsConnected => false;

    public IEnumerable<IFileSystemComponent> ListDirectory(string directoryPath, int depth)
    {
        return [];
    }

    public FileReadResult ReadFile(string filePath)
    {
        return new FileReadResult.Failure("File system is not connected");
    }

    public FileModificationResult MoveFile(string currentFilePath, string newFilePath)
    {
        return new FileModificationResult.Failure("File system is not connected");
    }

    public FileModificationResult CopyFile(string currentFilePath, string newFilePath)
    {
        return new FileModificationResult.Failure("File system is not connected");
    }

    public FileModificationResult DeleteFile(string filePath)
    {
        return new FileModificationResult.Failure("File system is not connected");
    }

    public FileModificationResult RenameFile(string filePath, string newFileName)
    {
        return new FileModificationResult.Failure("File system is not connected");
    }

    public bool FileExists(string filePath)
    {
        return false;
    }

    public bool DirectoryExists(string directoryPath)
    {
        return false;
    }
}