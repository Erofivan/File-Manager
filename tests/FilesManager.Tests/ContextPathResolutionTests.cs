using Itmo.ObjectOrientedProgramming.Lab4.Core;
using Itmo.ObjectOrientedProgramming.Lab4.Tests.Mocks;
using Xunit;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests;

public sealed class ContextPathResolutionTests
{
    // Relative path resolution appends to connection path
    [Fact]
    public void ResolvePath_RelativeFromRoot_AppendsToConnectionPath()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem().AddDirectory("/home/user");
        var context = new Context();
        context.Connect(mockFs, "/home/user");

        // Act
        string resolved = context.ResolvePath("docs");

        // Assert
        Assert.Equal("/home/user/docs", resolved);
    }

    // Absolute path resolution prepends connection path
    [Fact]
    public void ResolvePath_AbsolutePath_PrependsConnectionPath()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem().AddDirectory("/home/user");
        var context = new Context();
        context.Connect(mockFs, "/home/user");

        // Act
        string resolved = context.ResolvePath("/docs/readme.md");

        // Assert
        Assert.Equal("/home/user/docs/readme.md", resolved);
    }

    // Dot-dot navigates up correctly
    [Fact]
    public void ResolvePath_DotDot_NavigatesUp()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem().AddDirectory("/home/user");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        context.SetCurrentPath("/docs/reports");

        // Act
        string resolved = context.ResolvePath("..");

        // Assert
        Assert.Equal("/home/user/docs", resolved);
    }

    // Dot resolves to same current directory
    [Fact]
    public void ResolvePath_Dot_ResolvesToCurrentDirectory()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem().AddDirectory("/home/user");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        context.SetCurrentPath("/docs");

        // Act
        string resolved = context.ResolvePath(".");

        // Assert
        Assert.Equal("/home/user/docs", resolved);
    }

    // Path going above connection path gets clamped
    [Fact]
    public void ResolvePath_GoingAboveConnectionPath_ClampedToConnectionPath()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem().AddDirectory("/home/user");
        var context = new Context();
        context.Connect(mockFs, "/home/user");

        // Act
        string resolved = context.ResolvePath("../../..");

        // Assert
        Assert.Equal("/home/user", resolved);
    }

    // Relative path from subdirectory resolves correctly
    [Fact]
    public void ResolvePath_RelativeFromSubdir_ResolvesCorrectly()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem().AddDirectory("/home/user");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        context.SetCurrentPath("/docs");

        // Act
        string resolved = context.ResolvePath("file.txt");

        // Assert
        Assert.Equal("/home/user/docs/file.txt", resolved);
    }

    // Complex path with mixed dot-dot segments resolves correctly
    [Fact]
    public void ResolvePath_ComplexPathWithDotDot_ResolvesCorrectly()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem().AddDirectory("/home/user");
        var context = new Context();
        context.Connect(mockFs, "/home/user");
        context.SetCurrentPath("/docs/reports");

        // Act
        string resolved = context.ResolvePath("../images/photo.png");

        // Assert
        Assert.Equal("/home/user/docs/images/photo.png", resolved);
    }

    // Absolute path with dot-dot normalizes correctly
    [Fact]
    public void ResolvePath_AbsolutePathWithDotDot_NormalizesCorrectly()
    {
        // Arrange
        MockFileSystem mockFs = new MockFileSystem().AddDirectory("/home/user");
        var context = new Context();
        context.Connect(mockFs, "/home/user");

        // Act
        string resolved = context.ResolvePath("/a/b/../c");

        // Assert
        Assert.Equal("/home/user/a/c", resolved);
    }
}