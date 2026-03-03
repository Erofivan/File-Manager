using Itmo.ObjectOrientedProgramming.Lab4.Core;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Tree.List;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Components;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Trees;
using Itmo.ObjectOrientedProgramming.Lab4.Tests.Mocks;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public sealed class TreeListCommandTests
{
    // tree list outputs directory contents via visitor
    [Fact]
    public void Execute_WithComponents_OutputsTree()
    {
        // Arrange
        var components = new IFileSystemComponent[]
        {
            new DirectoryFileSystemComponent("subdir", Array.Empty<IFileSystemComponent>()),
            new FileFileSystemComponent("file.txt"),
        };
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectoryWithComponents("/home/user", components);
        var writer = new MockOutputWriter();
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        var command = new TreeListCommand(1, writer, new FileSystemTreeDisplaySettings("F ", "D ", "  "));

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.Success>(result);
        Assert.Contains("subdir", writer.FullOutput, StringComparison.Ordinal);
        Assert.Contains("file.txt", writer.FullOutput, StringComparison.Ordinal);
    }

    // tree list with depth 1 does not show nested children
    [Fact]
    public void Execute_DepthOne_DoesNotShowNestedChildren()
    {
        // Arrange
        var nestedComponents = new IFileSystemComponent[]
        {
            new FileFileSystemComponent("nested.txt"),
        };
        var components = new IFileSystemComponent[]
        {
            new DirectoryFileSystemComponent("subdir", nestedComponents),
        };
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectoryWithComponents("/root", components);
        var writer = new MockOutputWriter();
        var context = new Context();
        context.Connect(mockFs, "/root");
        var command = new TreeListCommand(1, writer, new FileSystemTreeDisplaySettings("F ", "D ", "  "));

        // Act
        command.Execute(context);

        // Assert
        Assert.Contains("subdir", writer.FullOutput, StringComparison.Ordinal);
        Assert.Contains("nested.txt", writer.FullOutput, StringComparison.Ordinal);
    }

    // tree list when not connected returns FileSystemNotConnected
    [Fact]
    public void Execute_WhenNotConnected_ReturnsFileSystemNotConnected()
    {
        // Arrange
        var writer = new MockOutputWriter();
        var context = new Context();
        var command = new TreeListCommand(1, writer, new FileSystemTreeDisplaySettings("F ", "D ", "  "));

        // Act
        CommandExecutionResult result = command.Execute(context);

        // Assert
        Assert.IsType<CommandExecutionResult.FileSystemNotConnected>(result);
    }

    // tree list uses correct display symbols in output
    [Fact]
    public void Execute_DisplaySettings_UsesCorrectSymbols()
    {
        // Arrange
        var components = new IFileSystemComponent[]
        {
            new FileFileSystemComponent("readme.md"),
        };
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectoryWithComponents("/data", components);
        var writer = new MockOutputWriter();
        var context = new Context();
        context.Connect(mockFs, "/data");
        var command = new TreeListCommand(1, writer, new FileSystemTreeDisplaySettings("[FILE] ", "[DIR] ", "  "));

        // Act
        command.Execute(context);

        // Assert
        Assert.Contains("[FILE] readme.md", writer.FullOutput, StringComparison.Ordinal);
    }

    // tree list uses correct directory symbol
    [Fact]
    public void Execute_DisplaySettings_UsesCorrectDirectorySymbol()
    {
        // Arrange
        var components = new IFileSystemComponent[]
        {
            new DirectoryFileSystemComponent("src", Array.Empty<IFileSystemComponent>()),
        };
        MockFileSystem mockFs = new MockFileSystem()
            .AddDirectoryWithComponents("/project", components);
        var writer = new MockOutputWriter();
        var context = new Context();
        context.Connect(mockFs, "/project");
        var command = new TreeListCommand(1, writer, new FileSystemTreeDisplaySettings("[FILE] ", "[DIR] ", "    "));

        // Act
        command.Execute(context);

        // Assert
        Assert.Contains("[DIR] src", writer.FullOutput, StringComparison.Ordinal);
    }
}