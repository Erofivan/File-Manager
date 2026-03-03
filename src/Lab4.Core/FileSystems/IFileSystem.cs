using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Components;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.FileSystemOperationsResults;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

public interface IFileSystem
{
    bool IsConnected { get; }

    bool IsAbsolutePath(string path);

    string NormalizePath(string path);

    string CombinePaths(string basePath, string relativePath);

    bool IsPathWithinBasePath(string path, string basePath);

    IEnumerable<IFileSystemComponent> ListDirectory(string directoryPath, int depth);

    FileReadOperationResult ReadFile(string filePath);

    FileMoveOperationResult MoveFile(string currentFilePath, string newFilePath);

    FileCopyOperationResult CopyFile(string currentFilePath, string newFilePath);

    FileDeleteOperationResult DeleteFile(string filePath);

    FileRenameOperationResult RenameFile(string filePath, string newFileName);

    bool FileExists(string filePath);

    bool DirectoryExists(string directoryPath);
}