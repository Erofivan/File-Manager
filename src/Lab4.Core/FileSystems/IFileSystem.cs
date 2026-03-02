using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Components;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

public interface IFileSystem
{
    bool IsConnected { get; }

    IEnumerable<IFileSystemComponent> ListDirectory(string path, int depth);

    FileReadResult ReadFile(string path);

    FileOperationResult MoveFile(string path);

    FileOperationResult CopyFile(string oldPath, string newPath);

    FileOperationResult DeleteFile(string path);

    FileOperationResult RenameFile(string path, string newName);

    bool FileExists(string path);

    bool DirectoryExists(string path);
}