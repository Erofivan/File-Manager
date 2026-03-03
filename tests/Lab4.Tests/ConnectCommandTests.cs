using Itmo.ObjectOrientedProgramming.Lab4.Core;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Connect;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.Tests.Mocks;
using NSubstitute;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public sealed class ConnectCommandTests
{
    [Fact]
    public void Execute_ValidAbsolutePath_ReturnsSuccessAndSetsConnection()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem().AddDirectory("/home/user");
        IFileSystemFactory factory = Substitute.For<IFileSystemFactory>();
        factory.Create().Returns(mockFs);
        var command = new ConnectCommand("/home/user", factory);
        var context = new Context();

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.Success>(result);
        Assert.True(context.IsConnected);
        Assert.Equal("/home/user", context.ConnectionPath);
    }

    [Fact]
    public void Execute_RelativePath_ReturnsFailure()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem().AddDirectory("relative/path");
        IFileSystemFactory factory = Substitute.For<IFileSystemFactory>();
        factory.Create().Returns(mockFs);
        var command = new ConnectCommand("relative/path", factory);
        var context = new Context();

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.Failure>(result);
    }

    [Fact]
    public void Execute_NonExistentDirectory_ReturnsDirectoryNotFound()
    {
        // Arrange
        var mockFs = new MockFileSystem();
        IFileSystemFactory factory = Substitute.For<IFileSystemFactory>();
        factory.Create().Returns(mockFs);
        var command = new ConnectCommand("/nonexistent", factory);
        var context = new Context();

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.DirectoryNotFound>(result);
    }

    [Fact]
    public void Execute_TrailingSlash_ConnectionPathIsTrimmed()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem().AddDirectory("/home/user/");
        IFileSystemFactory factory = Substitute.For<IFileSystemFactory>();
        factory.Create().Returns(mockFs);
        var command = new ConnectCommand("/home/user/", factory);
        var context = new Context();

        // Act
        command.Execute(context);

        // Assert
        Assert.Equal("/home/user", context.ConnectionPath);
    }
}