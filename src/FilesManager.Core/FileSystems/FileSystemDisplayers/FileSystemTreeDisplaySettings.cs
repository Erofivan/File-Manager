namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.FileSystemDisplayers;

public sealed class FileSystemTreeDisplaySettings
{
    public FileSystemTreeDisplaySettings(string fileSymbol, string directorySymbol, string indentSymbol)
    {
        FileSymbol = fileSymbol;
        DirectorySymbol = directorySymbol;
        IndentSymbol = indentSymbol;
    }

    public string FileSymbol { get; }

    public string DirectorySymbol { get; }

    public string IndentSymbol { get; }
}