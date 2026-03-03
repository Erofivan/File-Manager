using Itmo.ObjectOrientedProgramming.Lab4.Core;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Move;
using Itmo.ObjectOrientedProgramming.Lab4.Tests.Mocks;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public sealed class FileMoveCommandTests
{
    // File move with valid source and destination returns success
    [Fact]
    public void Execute_ValidPaths_ReturnsSuccess()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/home/user")
            .AddDirectory("/home/user/dest")
            .AddFile("/home/user/file.txt", "data");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        var command = new FileMoveCommand("file.txt", "dest");

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.Success>(result);
    }

    // File move with nonexistent source returns FileNotFound
    [Fact]
    public void Execute_NonExistentSource_ReturnsFileNotFound()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/home/user")
            .AddDirectory("/home/user/dest");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        var command = new FileMoveCommand("missing.txt", "dest");

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.FileNotFound>(result);
    }

    // File move with nonexistent destination dir returns DirectoryNotFound
    [Fact]
    public void Execute_NonExistentDestination_ReturnsDirectoryNotFound()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/home/user")
            .AddFile("/home/user/file.txt", "data");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        var command = new FileMoveCommand("file.txt", "nonexistent");

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.DirectoryNotFound>(result);
    }

    // File move when not connected returns FileSystemNotConnected
    [Fact]
    public void Execute_WhenNotConnected_ReturnsFileSystemNotConnected()
    {
        // Arrange
        var context = new Context();
        var command = new FileMoveCommand("a.txt", "b");

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.FileSystemNotConnected>(result);
    }
}