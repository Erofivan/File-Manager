using Itmo.ObjectOrientedProgramming.Lab4.Core;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Tree.GotoCommand;
using Itmo.ObjectOrientedProgramming.Lab4.Tests.Mocks;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public sealed class TreeGotoCommandTests
{
    [Fact]
    public void Execute_RelativePath_UpdatesCurrentPath()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/home/user")
            .AddDirectory("/home/user/docs");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        var command = new TreeGotoCommand("docs");

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.Success>(result);
        Assert.Equal("/docs", context.CurrentPath);
    }

    [Fact]
    public void Execute_AbsolutePath_SetsCurrentPathRelativeToConnection()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/home/user")
            .AddDirectory("/home/user/projects");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        var command = new TreeGotoCommand("/projects");

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.Success>(result);
        Assert.Equal("/projects", context.CurrentPath);
    }

    [Fact]
    public void Execute_DotDotPath_NavigatesUp()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/home/user")
            .AddDirectory("/home/user/docs")
            .AddDirectory("/home/user/docs/reports");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        new TreeGotoCommand("docs/reports").Execute(context);
        var command = new TreeGotoCommand("..");

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.Success>(result);
        Assert.Equal("/docs", context.CurrentPath);
    }

    [Fact]
    public void Execute_DotDotAtRoot_ClampsToConnectionPath()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/home/user");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        var command = new TreeGotoCommand("..");

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.Success>(result);
    }

    [Fact]
    public void Execute_NonExistentDirectory_ReturnsDirectoryNotFound()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/home/user");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        var command = new TreeGotoCommand("nonexistent");

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.DirectoryNotFound>(result);
    }

    [Fact]
    public void Execute_WhenNotConnected_ReturnsFileSystemNotConnected()
    {
        // Arrange
        var context = new Context();
        var command = new TreeGotoCommand("anywhere");

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.FileSystemNotConnected>(result);
    }

    [Fact]
    public void Execute_DotPath_KeepsCurrentPath()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/home/user")
            .AddDirectory("/home/user/docs");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        new TreeGotoCommand("docs").Execute(context);
        var command = new TreeGotoCommand(".");

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.Success>(result);
        Assert.Equal("/docs", context.CurrentPath);
    }
}