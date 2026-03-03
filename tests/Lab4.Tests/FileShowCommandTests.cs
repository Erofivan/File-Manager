using Itmo.ObjectOrientedProgramming.Lab4.Core;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Show;
using Itmo.ObjectOrientedProgramming.Lab4.Tests.Mocks;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public sealed class FileShowCommandTests
{
    // File show outputs file content to writer
    [Fact]
    public void Execute_ExistingFile_OutputsContent()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/home/user")
            .AddFile("/home/user/test.txt", "Hello World");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        var showWriter = new MockOutputWriter();
        var command = new FileShowCommand("test.txt", showWriter);

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.Success>(result);
        Assert.Contains("Hello World", showWriter.FullOutput, StringComparison.Ordinal);
    }

    // File show with absolute path resolves correctly
    [Fact]
    public void Execute_AbsolutePath_ResolvesCorrectly()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/home/user")
            .AddFile("/home/user/docs/readme.md", "Documentation");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        var showWriter = new MockOutputWriter();
        var command = new FileShowCommand("/docs/readme.md", showWriter);

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.Success>(result);
        Assert.Contains("Documentation", showWriter.FullOutput, StringComparison.Ordinal);
    }

    // File show for nonexistent file returns FileNotFound
    [Fact]
    public void Execute_NonExistentFile_ReturnsFileNotFound()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/home/user");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        var showWriter = new MockOutputWriter();
        var command = new FileShowCommand("missing.txt", showWriter);

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.FileNotFound>(result);
    }

    // File show when not connected returns FileSystemNotConnected
    [Fact]
    public void Execute_WhenNotConnected_ReturnsFileSystemNotConnected()
    {
        // Arrange
        var context = new Context();
        var showWriter = new MockOutputWriter();
        var command = new FileShowCommand("file.txt", showWriter);

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.FileSystemNotConnected>(result);
    }
}