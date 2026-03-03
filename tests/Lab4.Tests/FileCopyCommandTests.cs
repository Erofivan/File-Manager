using Itmo.ObjectOrientedProgramming.Lab4.Core;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Copy;
using Itmo.ObjectOrientedProgramming.Lab4.Tests.Mocks;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public sealed class FileCopyCommandTests
{
    // File copy with valid paths returns success
    [Fact]
    public void Execute_ValidPaths_ReturnsSuccess()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/home/user")
            .AddDirectory("/home/user/backup")
            .AddFile("/home/user/file.txt", "data");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        var command = new FileCopyCommand("file.txt", "backup");

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.Success>(result);
    }

    // File copy with nonexistent source returns FileNotFound
    [Fact]
    public void Execute_NonExistentSource_ReturnsFileNotFound()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/home/user")
            .AddDirectory("/home/user/backup");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        var command = new FileCopyCommand("missing.txt", "backup");

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.FileNotFound>(result);
    }

    // File copy with nonexistent destination returns DirectoryNotFound
    [Fact]
    public void Execute_NonExistentDestination_ReturnsDirectoryNotFound()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/home/user")
            .AddFile("/home/user/file.txt", "data");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        var command = new FileCopyCommand("file.txt", "nonexistent");

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.DirectoryNotFound>(result);
    }

    // File copy when not connected returns FileSystemNotConnected
    [Fact]
    public void Execute_WhenNotConnected_ReturnsFileSystemNotConnected()
    {
        // Arrange
        var context = new Context();
        var command = new FileCopyCommand("a.txt", "b");

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.FileSystemNotConnected>(result);
    }
}