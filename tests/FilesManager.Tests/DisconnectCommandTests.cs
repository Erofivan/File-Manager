using Itmo.ObjectOrientedProgramming.Lab4.Core;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Disconnect;
using Itmo.ObjectOrientedProgramming.Lab4.Tests.Mocks;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public sealed class DisconnectCommandTests
{
    [Fact]
    public void Execute_WhenConnected_ReturnsSuccessAndResetsState()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem().AddDirectory("/home/user");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        var command = new DisconnectCommand();

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.Success>(result);
        Assert.False(context.IsConnected);
    }

    [Fact]
    public void Execute_WhenNotConnected_ReturnsFileSystemNotConnected()
    {
        // Arrange
        var context = new Context();
        var command = new DisconnectCommand();

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.FileSystemNotConnected>(result);
    }
}