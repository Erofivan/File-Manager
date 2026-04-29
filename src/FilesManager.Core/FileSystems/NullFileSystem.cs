using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Components;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.FileSystemOperationsResults;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

public sealed class NullFileSystem : IFileSystem
{
    public bool IsConnected => false;

    public bool IsAbsolutePath(string path)
    {
        return false;
    }

    public string NormalizePath(string path)
    {
        return path;
    }

    public string CombinePaths(string basePath, string relativePath)
    {
        return basePath;
    }

    public bool IsPathWithinBasePath(string path, string basePath)
    {
        return false;
    }

    public IEnumerable<IFileSystemComponent> ListDirectory(string directoryPath, int depth)
    {
        return [];
    }

    public FileReadOperationResult ReadFile(string filePath)
    {
        return new FileReadOperationResult.Failure("File system is not connected");
    }

    public FileMoveOperationResult MoveFile(string currentFilePath, string newFilePath)
    {
        return new FileMoveOperationResult.Failure("File system is not connected");
    }

    public FileCopyOperationResult CopyFile(string currentFilePath, string newFilePath)
    {
        return new FileCopyOperationResult.Failure("File system is not connected");
    }

    public FileDeleteOperationResult DeleteFile(string filePath)
    {
        return new FileDeleteOperationResult.Failure("File system is not connected");
    }

    public FileRenameOperationResult RenameFile(string filePath, string newFileName)
    {
        return new FileRenameOperationResult.Failure("File system is not connected");
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