using Itmo.ObjectOrientedProgramming.Lab4.Core;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Connect;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Disconnect;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Copy;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Delete;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Move;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Rename;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Show;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Tree.GotoCommand;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Tree.List;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Components;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.FileSystemDisplayers;
using Itmo.ObjectOrientedProgramming.Lab4.Core.OutputWriters;
using Itmo.ObjectOrientedProgramming.Lab4.Tests.Mocks;
using NSubstitute;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public sealed class IntegrationTests
{
    [Fact]
    public void FullWorkflow_ConnectNavigateListShowDisconnect()
    {
        // Arrange
        var components = new IFileSystemComponent[]
        {
            new FileFileSystemComponent("readme.txt"),
        };
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/project")
            .AddDirectory("/project/src")
            .AddDirectoryWithComponents("/project/src", components)
            .AddFile("/project/src/readme.txt", "Source code readme");
        IFileSystemFactory factory = Substitute.For<IFileSystemFactory>();
        factory.Create().Returns(mockFs);
        var listWriter = new MockOutputWriter();
        var showWriter = new MockOutputWriter();
        var settings = new FileSystemTreeDisplaySettings("F ", "D ", "  ");
        var context = new Context();

        // Act & Assert - Connect
        CommandExecutionResult connectResult = new ConnectCommand("/project", factory).Execute(context);
        Assert.IsType<CommandExecutionResult.Success>(connectResult);

        // Act & Assert - Navigate
        CommandExecutionResult gotoResult = new TreeGotoCommand("src").Execute(context);
        Assert.IsType<CommandExecutionResult.Success>(gotoResult);
        Assert.Equal("/src", context.CurrentPath);

        // Act & Assert - List
        CommandExecutionResult listResult = new TreeListCommand(1, listWriter, settings).Execute(context);
        Assert.IsType<CommandExecutionResult.Success>(listResult);
        Assert.Contains("readme.txt", listWriter.FullOutput, StringComparison.Ordinal);

        // Act & Assert - Show
        CommandExecutionResult showResult = new FileShowCommand("readme.txt", showWriter).Execute(context);
        Assert.IsType<CommandExecutionResult.Success>(showResult);
        Assert.Contains("Source code readme", showWriter.FullOutput, StringComparison.Ordinal);

        // Act & Assert - Disconnect
        CommandExecutionResult disconnectResult = new DisconnectCommand().Execute(context);
        Assert.IsType<CommandExecutionResult.Success>(disconnectResult);
        Assert.False(context.IsConnected);
    }

    [Fact]
    public void CommandsBeforeConnect_ReturnFileSystemNotConnected()
    {
        // Arrange
        IOutputWriter writer = Substitute.For<IOutputWriter>();
        var settings = new FileSystemTreeDisplaySettings("F ", "D ", "  ");
        var context = new Context();

        // Act
        CommandExecutionResult gotoResult = new TreeGotoCommand("docs").Execute(context);
        CommandExecutionResult listResult = new TreeListCommand(1, writer, settings).Execute(context);
        CommandExecutionResult showResult = new FileShowCommand("file.txt", writer).Execute(context);
        CommandExecutionResult moveResult = new FileMoveCommand("a.txt", "b").Execute(context);
        CommandExecutionResult copyResult = new FileCopyCommand("a.txt", "b").Execute(context);
        CommandExecutionResult deleteResult = new FileDeleteCommand("a.txt").Execute(context);
        CommandExecutionResult renameResult = new FileRenameCommand("a.txt", "b.txt").Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.FileSystemNotConnected>(gotoResult);
        Assert.IsType<CommandExecutionResult.FileSystemNotConnected>(listResult);
        Assert.IsType<CommandExecutionResult.FileSystemNotConnected>(showResult);
        Assert.IsType<CommandExecutionResult.FileSystemNotConnected>(moveResult);
        Assert.IsType<CommandExecutionResult.FileSystemNotConnected>(copyResult);
        Assert.IsType<CommandExecutionResult.FileSystemNotConnected>(deleteResult);
        Assert.IsType<CommandExecutionResult.FileSystemNotConnected>(renameResult);
    }

    [Fact]
    public void NavigateUpThenList_ShowsParentContent()
    {
        // Arrange
        var rootComponents = new IFileSystemComponent[]
        {
            new DirectoryFileSystemComponent("subdir", []),
            new FileFileSystemComponent("root.txt"),
        };
        var subdirComponents = new IFileSystemComponent[]
        {
            new FileFileSystemComponent("child.txt"),
        };
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/root")
            .AddDirectoryWithComponents("/root", rootComponents)
            .AddDirectory("/root/subdir")
            .AddDirectoryWithComponents("/root/subdir", subdirComponents);
        var writer = new MockOutputWriter();
        var settings = new FileSystemTreeDisplaySettings("F ", "D ", "  ");
        var context = new Context();
        context.Connect(mockFs, "/root");

        // Act
        new TreeGotoCommand("subdir").Execute(context);
        new TreeGotoCommand("..").Execute(context);
        new TreeListCommand(1, writer, settings).Execute(context);

        // Assert
        Assert.Contains("root.txt", writer.FullOutput, StringComparison.Ordinal);
    }

    [Fact]
    public void Reconnect_ResetsToNewPath()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/home/user")
            .AddDirectory("/home/user/docs")
            .AddDirectory("/var/data");
        IFileSystemFactory factory = Substitute.For<IFileSystemFactory>();
        factory.Create().Returns(mockFs);
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        new TreeGotoCommand("docs").Execute(context);

        // Act
        new ConnectCommand("/var/data", factory).Execute(context);

        // Assert
        Assert.Equal("/var/data", context.ConnectionPath);
        Assert.Equal(string.Empty, context.CurrentPath);
    }

    [Fact]
    public void FileMove_FromSubdirectory_ResolvesPathsCorrectly()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectory("/project")
            .AddDirectory("/project/src")
            .AddDirectory("/project/dest")
            .AddFile("/project/src/app.cs");
        var context = new Context();
        context.Connect(mockFs, "/project");
        new TreeGotoCommand("src").Execute(context);

        // Act
        CommandExecutionResult result = new FileMoveCommand("app.cs", "/dest").Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.Success>(result);
        Assert.Equal("/project/src/app.cs", mockFs.LastMoveSource);
        Assert.Equal("/project/dest", mockFs.LastMoveDest);
    }
}