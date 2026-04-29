using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Components;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.FileSystemOperationsResults;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

public sealed class LocalFileSystem : IFileSystem
{
    public bool IsConnected => true;

    public bool IsAbsolutePath(string path)
    {
        return Path.IsPathRooted(path);
    }

    public string NormalizePath(string path)
    {
        return Path.GetFullPath(path);
    }

    public string CombinePaths(string basePath, string relativePath)
    {
        if (IsAbsolutePath(relativePath))
        {
            relativePath = relativePath
                .TrimStart(Path.DirectorySeparatorChar)
                .TrimStart(Path.AltDirectorySeparatorChar);
        }

        return Path.Combine(basePath, relativePath);
    }

    public bool IsPathWithinBasePath(string path, string basePath)
    {
        string normalizedPath = NormalizePath(path);
        string normalizedBase = NormalizePath(basePath);

        return string.Equals(normalizedPath, normalizedBase, StringComparison.Ordinal)
               || normalizedPath.StartsWith(
                   normalizedBase + Path.DirectorySeparatorChar,
                   StringComparison.Ordinal);
    }

    public IEnumerable<IFileSystemComponent> ListDirectory(string directoryPath, int depth)
    {
        if (depth <= 0)
            yield break;

        var directoryInfo = new DirectoryInfo(directoryPath);

        if (directoryInfo.Exists is false)
            yield break;

        foreach (DirectoryInfo directory in directoryInfo.EnumerateDirectories())
        {
            yield return new DirectoryFileSystemComponent(
                directory.Name,
                ListDirectory(directory.FullName, depth - 1));
        }

        foreach (FileInfo file in directoryInfo.EnumerateFiles())
        {
            yield return new FileFileSystemComponent(file.Name);
        }
    }

    public FileReadOperationResult ReadFile(string filePath)
    {
        try
        {
            string content = File.ReadAllText(filePath);

            return new FileReadOperationResult.Success(content);
        }
        catch (IOException ex)
        {
            return new FileReadOperationResult.Failure(ex.Message);
        }
    }

    public FileMoveOperationResult MoveFile(string currentFilePath, string newFilePath)
    {
        try
        {
            string fileName = Path.GetFileName(currentFilePath);
            newFilePath = Path.Combine(newFilePath, fileName);

            File.Move(currentFilePath, newFilePath);

            return new FileMoveOperationResult.Success();
        }
        catch (IOException ex)
        {
            return new FileMoveOperationResult.Failure(ex.Message);
        }
    }

    public FileCopyOperationResult CopyFile(string currentFilePath, string newFilePath)
    {
        try
        {
            string fileName = Path.GetFileName(currentFilePath);
            newFilePath = Path.Combine(newFilePath, fileName);

            File.Copy(currentFilePath, newFilePath);

            return new FileCopyOperationResult.Success();
        }
        catch (IOException ex)
        {
            return new FileCopyOperationResult.Failure(ex.Message);
        }
    }

    public FileDeleteOperationResult DeleteFile(string filePath)
    {
        try
        {
            File.Delete(filePath);

            return new FileDeleteOperationResult.Success();
        }
        catch (IOException ex)
        {
            return new FileDeleteOperationResult.Failure(ex.Message);
        }
    }

    public FileRenameOperationResult RenameFile(string filePath, string newFileName)
    {
        try
        {
            if (newFileName.Contains(Path.DirectorySeparatorChar, StringComparison.Ordinal)
                || newFileName.Contains(Path.AltDirectorySeparatorChar, StringComparison.Ordinal))
            {
                return new FileRenameOperationResult.Failure("New name must not contain path separators");
            }

            string? directoryName = Path.GetDirectoryName(filePath);

            if (directoryName is null)
                return new FileRenameOperationResult.Failure("Cannot determine directory for path");

            string newFilePath = Path.Combine(directoryName, newFileName);

            File.Move(filePath, newFilePath);

            return new FileRenameOperationResult.Success();
        }
        catch (IOException ex)
        {
            return new FileRenameOperationResult.Failure(ex.Message);
        }
    }

    public bool FileExists(string filePath)
    {
        return File.Exists(filePath);
    }

    public bool DirectoryExists(string directoryPath)
    {
        return Directory.Exists(directoryPath);
    }
}