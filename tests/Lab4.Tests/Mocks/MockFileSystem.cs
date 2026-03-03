using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Components;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.FileSystemOperationsResults;

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

    public bool IsAbsolutePath(string path)
    {
        return path.StartsWith('/');
    }

    public string NormalizePath(string path)
    {
        string[] segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var stack = new List<string>();

        foreach (string segment in segments)
        {
            if (segment is ".")
            {
                continue;
            }

            if (segment is ".." && stack.Count > 0)
            {
                stack.RemoveAt(stack.Count - 1);
            }
            else if (segment is not "..")
            {
                stack.Add(segment);
            }
        }

        return "/" + string.Join("/", stack);
    }

    public string CombinePaths(string basePath, string relativePath)
    {
        if (string.IsNullOrEmpty(relativePath))
            return basePath;

        if (IsAbsolutePath(relativePath))
            return basePath + relativePath;

        return basePath + "/" + relativePath;
    }

    public bool IsPathWithinBasePath(string path, string basePath)
    {
        return path.StartsWith(basePath, StringComparison.Ordinal);
    }

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

    public FileReadOperationResult ReadFile(string filePath)
    {
        if (_fileContents.TryGetValue(filePath, out string? content))
            return new FileReadOperationResult.Success(content);

        return new FileReadOperationResult.Failure("File not found");
    }

    public FileMoveOperationResult MoveFile(string currentFilePath, string newFilePath)
    {
        LastMoveSource = currentFilePath;
        LastMoveDest = newFilePath;
        return new FileMoveOperationResult.Success();
    }

    public FileCopyOperationResult CopyFile(string currentFilePath, string newFilePath)
    {
        LastCopySource = currentFilePath;
        LastCopyDest = newFilePath;
        return new FileCopyOperationResult.Success();
    }

    public FileDeleteOperationResult DeleteFile(string filePath)
    {
        LastDeletePath = filePath;
        return new FileDeleteOperationResult.Success();
    }

    public FileRenameOperationResult RenameFile(string filePath, string newFileName)
    {
        if (newFileName.Contains('/', StringComparison.Ordinal))
            return new FileRenameOperationResult.Failure("New name must not contain path separators");

        LastRenamePath = filePath;
        LastRenameNewName = newFileName;
        return new FileRenameOperationResult.Success();
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
