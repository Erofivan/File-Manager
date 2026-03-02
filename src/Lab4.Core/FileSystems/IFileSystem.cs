using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Components;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

public interface IFileSystem
{
    bool IsConnected { get; }

    IEnumerable<IFileSystemComponent> ListDirectory(string directoryPath, int depth);

    FileReadResult ReadFile(string filePath);

    FileModificationResult MoveFile(string currentFilePath, string newFilePath);

    FileModificationResult CopyFile(string currentFilePath, string newFilePath);

    FileModificationResult DeleteFile(string filePath);

    FileModificationResult RenameFile(string filePath, string newFileName);

    bool FileExists(string filePath);

    bool DirectoryExists(string directoryPath);
}