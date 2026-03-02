using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Components;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;

public sealed class LocalFileSystem : IFileSystem
{
    public bool IsConnected => true;

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

    public FileReadResult ReadFile(string filePath)
    {
        try
        {
            string content = File.ReadAllText(filePath);

            return new FileReadResult.Success(content);
        }
        catch (IOException ex)
        {
            return new FileReadResult.Failure(ex.Message);
        }
    }

    public FileModificationResult MoveFile(string currentFilePath, string newFilePath)
    {
        try
        {
            string fileName = Path.GetFileName(currentFilePath);
            newFilePath = Path.Combine(newFilePath, fileName);

            File.Move(currentFilePath, newFilePath);

            return new FileModificationResult.Success();
        }
        catch (IOException ex)
        {
            return new FileModificationResult.Failure(ex.Message);
        }
    }

    public FileModificationResult CopyFile(string currentFilePath, string newFilePath)
    {
        try
        {
            string fileName = Path.GetFileName(currentFilePath);
            newFilePath = Path.Combine(newFilePath, fileName);

            File.Copy(currentFilePath, newFilePath);

            return new FileModificationResult.Success();
        }
        catch (IOException ex)
        {
            return new FileModificationResult.Failure(ex.Message);
        }
    }

    public FileModificationResult DeleteFile(string filePath)
    {
        try
        {
            File.Delete(filePath);

            return new FileModificationResult.Success();
        }
        catch (IOException ex)
        {
            return new FileModificationResult.Failure(ex.Message);
        }
    }

    public FileModificationResult RenameFile(string filePath, string newFileName)
    {
        try
        {
            string? directoryName = Path.GetDirectoryName(filePath);

            if (directoryName is null)
                return new FileModificationResult.Failure("Cannot determine directory for path");

            string newFilePath = Path.Combine(directoryName, newFileName);

            File.Move(filePath, newFilePath);

            return new FileModificationResult.Success();
        }
        catch (IOException ex)
        {
            return new FileModificationResult.Failure(ex.Message);
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