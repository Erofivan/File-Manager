using Itmo.ObjectOrientedProgramming.Lab4.Core;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Rename;
using Itmo.ObjectOrientedProgramming.Lab4.Tests.Mocks;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public sealed class FileRenameCommandTests
{
    // File rename with valid name returns success
    [Fact]
    public void Execute_ValidName_ReturnsSuccess()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/home/user")
            .AddFile("/home/user/old.txt");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        var command = new FileRenameCommand("old.txt", "new.txt");

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.Success>(result);
        Assert.Equal("new.txt", mockFs.LastRenameNewName);
    }

    // File rename with slash in name returns failure
    [Fact]
    public void Execute_NameWithSlash_ReturnsFailure()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/home/user")
            .AddFile("/home/user/old.txt");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        var command = new FileRenameCommand("old.txt", "sub/new.txt");

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.Failure>(result);
    }

    // File rename for nonexistent file returns FileNotFound
    [Fact]
    public void Execute_NonExistentFile_ReturnsFileNotFound()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/home/user");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        var command = new FileRenameCommand("missing.txt", "new.txt");

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.FileNotFound>(result);
    }

    // File rename when not connected returns FileSystemNotConnected
    [Fact]
    public void Execute_WhenNotConnected_ReturnsFileSystemNotConnected()
    {
        // Arrange
        var context = new Context();
        var command = new FileRenameCommand("file.txt", "other.txt");

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.FileSystemNotConnected>(result);
    }
}