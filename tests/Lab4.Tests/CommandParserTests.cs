using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public sealed class CommandParserTests
{
    private readonly ICommandHandler _handler = new CommandHandlerFactory().Create();

    // Parsing "connect /path" creates ConnectCommand
    [Fact]
    public void Handle_ConnectCommand_CreatesCorrectCommand()
    {
        // Arrange
        string[] args = { "connect", "/home/user" };

        // Act
        ICommand? command = _handler.Handle(args);

        // Assert
        Assert.NotNull(command);
        Assert.IsType<ConnectCommand>(command);
    }

    // Parsing "connect /path -m local" creates ConnectCommand
    [Fact]
    public void Handle_ConnectCommandWithMode_CreatesCorrectCommand()
    {
        // Arrange
        string[] args = { "connect", "/home/user", "-m", "local" };

        // Act
        ICommand? command = _handler.Handle(args);

        // Assert
        Assert.NotNull(command);
        Assert.IsType<ConnectCommand>(command);
    }

    // Parsing "disconnect" creates DisconnectCommand
    [Fact]
    public void Handle_DisconnectCommand_CreatesCorrectCommand()
    {
        // Arrange
        string[] args = { "disconnect" };

        // Act
        ICommand? command = _handler.Handle(args);

        // Assert
        Assert.NotNull(command);
        Assert.IsType<DisconnectCommand>(command);
    }

    // Parsing "tree goto /path" creates TreeGotoCommand
    [Fact]
    public void Handle_TreeGotoCommand_CreatesCorrectCommand()
    {
        // Arrange
        string[] args = { "tree", "goto", "/some/path" };

        // Act
        ICommand? command = _handler.Handle(args);

        // Assert
        Assert.NotNull(command);
        Assert.IsType<TreeGotoCommand>(command);
    }

    // Parsing "tree list -d 3" creates TreeListCommand
    [Fact]
    public void Handle_TreeListCommandWithDepth_CreatesCorrectCommand()
    {
        // Arrange
        string[] args = { "tree", "list", "-d", "3" };

        // Act
        ICommand? command = _handler.Handle(args);

        // Assert
        Assert.NotNull(command);
        Assert.IsType<TreeListCommand>(command);
    }

    // Parsing "tree list" without depth creates TreeListCommand with default depth
    [Fact]
    public void Handle_TreeListCommandWithoutDepth_CreatesCommand()
    {
        // Arrange
        string[] args = { "tree", "list" };

        // Act
        ICommand? command = _handler.Handle(args);

        // Assert
        Assert.NotNull(command);
        Assert.IsType<TreeListCommand>(command);
    }

    // Parsing "file show /path -m console" creates FileShowCommand
    [Fact]
    public void Handle_FileShowCommand_CreatesCorrectCommand()
    {
        // Arrange
        string[] args = { "file", "show", "/some/file.txt", "-m", "console" };

        // Act
        ICommand? command = _handler.Handle(args);

        // Assert
        Assert.NotNull(command);
        Assert.IsType<FileShowCommand>(command);
    }

    // Parsing "file show /path" with default mode creates FileShowCommand
    [Fact]
    public void Handle_FileShowCommandDefaultMode_CreatesCorrectCommand()
    {
        // Arrange
        string[] args = { "file", "show", "/some/file.txt" };

        // Act
        ICommand? command = _handler.Handle(args);

        // Assert
        Assert.NotNull(command);
        Assert.IsType<FileShowCommand>(command);
    }

    // Parsing "file move /src /dest" creates FileMoveCommand
    [Fact]
    public void Handle_FileMoveCommand_CreatesCorrectCommand()
    {
        // Arrange
        string[] args = { "file", "move", "/source.txt", "/dest" };

        // Act
        ICommand? command = _handler.Handle(args);

        // Assert
        Assert.NotNull(command);
        Assert.IsType<FileMoveCommand>(command);
    }

    // Parsing "file copy /src /dest" creates FileCopyCommand
    [Fact]
    public void Handle_FileCopyCommand_CreatesCorrectCommand()
    {
        // Arrange
        string[] args = { "file", "copy", "/source.txt", "/dest" };

        // Act
        ICommand? command = _handler.Handle(args);

        // Assert
        Assert.NotNull(command);
        Assert.IsType<FileCopyCommand>(command);
    }

    // Parsing "file delete /path" creates FileDeleteCommand
    [Fact]
    public void Handle_FileDeleteCommand_CreatesCorrectCommand()
    {
        // Arrange
        string[] args = { "file", "delete", "/some/file.txt" };

        // Act
        ICommand? command = _handler.Handle(args);

        // Assert
        Assert.NotNull(command);
        Assert.IsType<FileDeleteCommand>(command);
    }

    // Parsing "file rename /path newname" creates FileRenameCommand
    [Fact]
    public void Handle_FileRenameCommand_CreatesCorrectCommand()
    {
        // Arrange
        string[] args = { "file", "rename", "/some/file.txt", "newfile.txt" };

        // Act
        ICommand? command = _handler.Handle(args);

        // Assert
        Assert.NotNull(command);
        Assert.IsType<FileRenameCommand>(command);
    }

    // Parsing unknown command returns null
    [Fact]
    public void Handle_UnknownCommand_ReturnsNull()
    {
        // Arrange
        string[] args = { "unknown", "command" };

        // Act
        ICommand? command = _handler.Handle(args);

        // Assert
        Assert.Null(command);
    }

    // Parsing empty args returns null
    [Fact]
    public void Handle_EmptyArgs_ReturnsNull()
    {
        // Arrange
        string[] args = Array.Empty<string>();

        // Act
        ICommand? command = _handler.Handle(args);

        // Assert
        Assert.Null(command);
    }

    // Parsing single non-command word returns null
    [Fact]
    public void Handle_SingleRandomWord_ReturnsNull()
    {
        // Arrange
        string[] args = { "hello" };

        // Act
        ICommand? command = _handler.Handle(args);

        // Assert
        Assert.Null(command);
    }
}