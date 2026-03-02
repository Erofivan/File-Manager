using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Components;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

public sealed class LocalFileSystem : IFileSystem
{
    public bool IsConnected => true;

    public IEnumerable<IFileSystemComponent> ListDirectory(string path, int depth)
    {
        throw new NotImplementedException();
    }

    public FileReadResult ReadFile(string path)
    {
        throw new NotImplementedException();
    }

    public FileOperationResult MoveFile(string path)
    {
        throw new NotImplementedException();
    }

    public FileOperationResult CopyFile(string oldPath, string newPath)
    {
        throw new NotImplementedException();
    }

    public FileOperationResult DeleteFile(string path)
    {
        throw new NotImplementedException();
    }

    public FileOperationResult RenameFile(string path, string newName)
    {
        throw new NotImplementedException();
    }

    public bool FileExists(string path)
    {
        return File.Exists(path);
    }

    public bool DirectoryExists(string path)
    {
        return Directory.Exists(path);
    }
}