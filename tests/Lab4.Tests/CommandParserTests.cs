using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Connect;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Disconnect;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Copy;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Delete;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Move;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Rename;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Show;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Tree.GotoCommand;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Tree.List;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Trees;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing;
using Itmo.ObjectOrientedProgramming.Lab4.Tests.Mocks;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public sealed class CommandParserTests
{
    private readonly ICommandHandler _handler = new CommandHandlerFactory(
        new CommandHandlerSettings(
            new MockOutputWriter(),
            new FileSystemTreeDisplaySettings("F: ", "  f: ", "  "))).Create();

    // Parsing "connect /path" creates ConnectCommand
    [Fact]
    public void Handle_ConnectCommand_CreatesCorrectCommand()
    {
        // Arrange
        string[] args = { "connect", "/home/user" };

        // Act
        CommandParseResult result = _handler.Handle(args);

        // Assert
        CommandParseResult.Success success = Assert.IsType<CommandParseResult.Success>(result);
        Assert.IsType<ConnectCommand>(success.Command);
    }

    // Parsing "connect /path -m local" creates ConnectCommand
    [Fact]
    public void Handle_ConnectCommandWithMode_CreatesCorrectCommand()
    {
        // Arrange
        string[] args = { "connect", "/home/user", "-m", "local" };

        // Act
        CommandParseResult result = _handler.Handle(args);

        // Assert
        CommandParseResult.Success success = Assert.IsType<CommandParseResult.Success>(result);
        Assert.IsType<ConnectCommand>(success.Command);
    }

    // Parsing "disconnect" creates DisconnectCommand
    [Fact]
    public void Handle_DisconnectCommand_CreatesCorrectCommand()
    {
        // Arrange
        string[] args = { "disconnect" };

        // Act
        CommandParseResult result = _handler.Handle(args);

        // Assert
        CommandParseResult.Success success = Assert.IsType<CommandParseResult.Success>(result);
        Assert.IsType<DisconnectCommand>(success.Command);
    }

    // Parsing "tree goto /path" creates TreeGotoCommand
    [Fact]
    public void Handle_TreeGotoCommand_CreatesCorrectCommand()
    {
        // Arrange
        string[] args = { "tree", "goto", "/some/path" };

        // Act
        CommandParseResult result = _handler.Handle(args);

        // Assert
        CommandParseResult.Success success = Assert.IsType<CommandParseResult.Success>(result);
        Assert.IsType<TreeGotoCommand>(success.Command);
    }

    // Parsing "tree list -d 3" creates TreeListCommand
    [Fact]
    public void Handle_TreeListCommandWithDepth_CreatesCorrectCommand()
    {
        // Arrange
        string[] args = { "tree", "list", "-d", "3" };

        // Act
        CommandParseResult result = _handler.Handle(args);

        // Assert
        CommandParseResult.Success success = Assert.IsType<CommandParseResult.Success>(result);
        Assert.IsType<TreeListCommand>(success.Command);
    }

    // Parsing "tree list" without depth creates TreeListCommand with default depth
    [Fact]
    public void Handle_TreeListCommandWithoutDepth_CreatesCommand()
    {
        // Arrange
        string[] args = { "tree", "list" };

        // Act
        CommandParseResult result = _handler.Handle(args);

        // Assert
        CommandParseResult.Success success = Assert.IsType<CommandParseResult.Success>(result);
        Assert.IsType<TreeListCommand>(success.Command);
    }

    // Parsing "file show /path -m console" creates FileShowCommand
    [Fact]
    public void Handle_FileShowCommand_CreatesCorrectCommand()
    {
        // Arrange
        string[] args = { "file", "show", "/some/file.txt", "-m", "console" };

        // Act
        CommandParseResult result = _handler.Handle(args);

        // Assert
        CommandParseResult.Success success = Assert.IsType<CommandParseResult.Success>(result);
        Assert.IsType<FileShowCommand>(success.Command);
    }

    // Parsing "file show /path" with default mode creates FileShowCommand
    [Fact]
    public void Handle_FileShowCommandDefaultMode_CreatesCorrectCommand()
    {
        // Arrange
        string[] args = { "file", "show", "/some/file.txt" };

        // Act
        CommandParseResult result = _handler.Handle(args);

        // Assert
        CommandParseResult.Success success = Assert.IsType<CommandParseResult.Success>(result);
        Assert.IsType<FileShowCommand>(success.Command);
    }

    // Parsing "file move /src /dest" creates FileMoveCommand
    [Fact]
    public void Handle_FileMoveCommand_CreatesCorrectCommand()
    {
        // Arrange
        string[] args = { "file", "move", "/source.txt", "/dest" };

        // Act
        CommandParseResult result = _handler.Handle(args);

        // Assert
        CommandParseResult.Success success = Assert.IsType<CommandParseResult.Success>(result);
        Assert.IsType<FileMoveCommand>(success.Command);
    }

    // Parsing "file copy /src /dest" creates FileCopyCommand
    [Fact]
    public void Handle_FileCopyCommand_CreatesCorrectCommand()
    {
        // Arrange
        string[] args = { "file", "copy", "/source.txt", "/dest" };

        // Act
        CommandParseResult result = _handler.Handle(args);

        // Assert
        CommandParseResult.Success success = Assert.IsType<CommandParseResult.Success>(result);
        Assert.IsType<FileCopyCommand>(success.Command);
    }

    // Parsing "file delete /path" creates FileDeleteCommand
    [Fact]
    public void Handle_FileDeleteCommand_CreatesCorrectCommand()
    {
        // Arrange
        string[] args = { "file", "delete", "/some/file.txt" };

        // Act
        CommandParseResult result = _handler.Handle(args);

        // Assert
        CommandParseResult.Success success = Assert.IsType<CommandParseResult.Success>(result);
        Assert.IsType<FileDeleteCommand>(success.Command);
    }

    // Parsing "file rename /path newname" creates FileRenameCommand
    [Fact]
    public void Handle_FileRenameCommand_CreatesCorrectCommand()
    {
        // Arrange
        string[] args = { "file", "rename", "/some/file.txt", "newfile.txt" };

        // Act
        CommandParseResult result = _handler.Handle(args);

        // Assert
        CommandParseResult.Success success = Assert.IsType<CommandParseResult.Success>(result);
        Assert.IsType<FileRenameCommand>(success.Command);
    }

    // Parsing unknown command returns Failure
    [Fact]
    public void Handle_UnknownCommand_ReturnsFailure()
    {
        // Arrange
        string[] args = { "unknown", "command" };

        // Act
        CommandParseResult result = _handler.Handle(args);

        // Assert
        Assert.IsType<CommandParseResult.Failure>(result);
    }

    // Parsing empty args returns Failure
    [Fact]
    public void Handle_EmptyArgs_ReturnsFailure()
    {
        // Arrange
        string[] args = [];

        // Act
        CommandParseResult result = _handler.Handle(args);

        // Assert
        Assert.IsType<CommandParseResult.Failure>(result);
    }

    // Parsing single non-command word returns Failure
    [Fact]
    public void Handle_SingleRandomWord_ReturnsFailure()
    {
        // Arrange
        string[] args = { "hello" };

        // Act
        CommandParseResult result = _handler.Handle(args);

        // Assert
        Assert.IsType<CommandParseResult.Failure>(result);
    }
}