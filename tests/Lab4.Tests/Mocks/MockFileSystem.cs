using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Components;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests.Mocks;

public sealed class MockFileSystem : IFileSystem
{
    private readonly HashSet<string> _files = new HashSet<string>(StringComparer.Ordinal);
    private readonly HashSet<string> _directories = new HashSet<string>(StringComparer.Ordinal);
    private readonly Dictionary<string, string> _fileContents = new Dictionary<string, string>(StringComparer.Ordinal);
    private readonly Dictionary<string, IEnumerable<IFileSystemComponent>> _directoryComponents = new Dictionary<string, IEnumerable<IFileSystemComponent>>(StringComparer.Ordinal);

    public bool IsConnected => true;

    public string? LastMoveSource { get; private set; }

    public string? LastMoveDest { get; private set; }

    public string? LastCopySource { get; private set; }

    public string? LastCopyDest { get; private set; }

    public string? LastDeletePath { get; private set; }

    public string? LastRenamePath { get; private set; }

    public string? LastRenameNewName { get; private set; }

    public MockFileSystem AddFile(string path, string content = "")
    {
        _files.Add(path);
        _fileContents[path] = content;
        return this;
    }

    public MockFileSystem AddDirectory(string path)
    {
        _directories.Add(path);
        return this;
    }

    public MockFileSystem AddDirectoryWithComponents(string path, IEnumerable<IFileSystemComponent> components)
    {
        _directories.Add(path);
        _directoryComponents[path] = components;
        return this;
    }

    public IEnumerable<IFileSystemComponent> ListDirectory(string directoryPath, int depth)
    {
        if (depth <= 0 || _directories.Contains(directoryPath) is false)
            yield break;

        if (_directoryComponents.TryGetValue(directoryPath, out IEnumerable<IFileSystemComponent>? components))
        {
            foreach (IFileSystemComponent component in components)
                yield return component;
        }
    }

    public FileReadResult ReadFile(string filePath)
    {
        if (_fileContents.TryGetValue(filePath, out string? content))
            return new FileReadResult.Success(content);

        return new FileReadResult.Failure("File not found");
    }

    public FileModificationResult MoveFile(string currentFilePath, string newFilePath)
    {
        LastMoveSource = currentFilePath;
        LastMoveDest = newFilePath;
        return new FileModificationResult.Success();
    }

    public FileModificationResult CopyFile(string currentFilePath, string newFilePath)
    {
        LastCopySource = currentFilePath;
        LastCopyDest = newFilePath;
        return new FileModificationResult.Success();
    }

    public FileModificationResult DeleteFile(string filePath)
    {
        LastDeletePath = filePath;
        return new FileModificationResult.Success();
    }

    public FileModificationResult RenameFile(string filePath, string newFileName)
    {
        LastRenamePath = filePath;
        LastRenameNewName = newFileName;
        return new FileModificationResult.Success();
    }

    public bool FileExists(string filePath)
    {
        return _files.Contains(filePath);
    }

    public bool DirectoryExists(string directoryPath)
    {
        return _directories.Contains(directoryPath);
    }
}
