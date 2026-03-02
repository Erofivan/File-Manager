using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Components;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public sealed class NullFileSystemTests
{
    // NullFileSystem reports not connected
    [Fact]
    public void IsConnected_ReturnsFalse()
    {
        // Arrange
        var nullFs = new NullFileSystem();

        // Act
        bool connected = nullFs.IsConnected;

        // Assert
        Assert.False(connected);
    }

    // NullFileSystem ListDirectory returns empty
    [Fact]
    public void ListDirectory_ReturnsEmpty()
    {
        // Arrange
        var nullFs = new NullFileSystem();

        // Act
        IEnumerable<IFileSystemComponent> components = nullFs.ListDirectory("/any", 1);

        // Assert
        Assert.Empty(components);
    }

    // NullFileSystem ReadFile returns failure
    [Fact]
    public void ReadFile_ReturnsFailure()
    {
        // Arrange
        var nullFs = new NullFileSystem();

        // Act
        FileReadResult result = nullFs.ReadFile("/any/file.txt");

        // Assert
        Assert.IsType<FileReadResult.Failure>(result);
    }

    // NullFileSystem MoveFile returns failure
    [Fact]
    public void MoveFile_ReturnsFailure()
    {
        // Arrange
        var nullFs = new NullFileSystem();

        // Act
        FileModificationResult result = nullFs.MoveFile("/a.txt", "/b");

        // Assert
        Assert.IsType<FileModificationResult.Failure>(result);
    }

    // NullFileSystem FileExists returns false
    [Fact]
    public void FileExists_ReturnsFalse()
    {
        // Arrange
        var nullFs = new NullFileSystem();

        // Act
        bool exists = nullFs.FileExists("/any/file.txt");

        // Assert
        Assert.False(exists);
    }

    // NullFileSystem DirectoryExists returns false
    [Fact]
    public void DirectoryExists_ReturnsFalse()
    {
        // Arrange
        var nullFs = new NullFileSystem();

        // Act
        bool exists = nullFs.DirectoryExists("/any");

        // Assert
        Assert.False(exists);
    }
}