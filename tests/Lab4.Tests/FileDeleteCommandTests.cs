using Itmo.ObjectOrientedProgramming.Lab4.Core;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Delete;
using Itmo.ObjectOrientedProgramming.Lab4.Tests.Mocks;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public sealed class FileDeleteCommandTests
{
    // File delete for existing file returns success
    [Fact]
    public void Execute_ExistingFile_ReturnsSuccess()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/home/user")
            .AddFile("/home/user/file.txt");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        var command = new FileDeleteCommand("file.txt");

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.Success>(result);
    }

    // File delete for nonexistent file returns FileNotFound
    [Fact]
    public void Execute_NonExistentFile_ReturnsFileNotFound()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/home/user");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        var command = new FileDeleteCommand("missing.txt");

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.FileNotFound>(result);
    }

    // File delete when not connected returns FileSystemNotConnected
    [Fact]
    public void Execute_WhenNotConnected_ReturnsFileSystemNotConnected()
    {
        // Arrange
        var context = new Context();
        var command = new FileDeleteCommand("file.txt");

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.FileSystemNotConnected>(result);
    }
}