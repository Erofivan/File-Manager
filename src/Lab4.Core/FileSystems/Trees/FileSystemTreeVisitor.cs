using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Components;
using Itmo.ObjectOrientedProgramming.Lab4.Core.OutputWriters;
using System.Text;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Trees;

public sealed class FileSystemTreeVisitor : IFileSystemComponentsVisitor
{
    private readonly IOutputWriter _writer;
    private readonly FileSystemTreeDisplaySettings _treeDisplaySettings;
    private readonly StringBuilder _buffer = new StringBuilder();

    private int _currentIndentDepth;

    public FileSystemTreeVisitor(IOutputWriter writer, FileSystemTreeDisplaySettings treeDisplaySettings)
    {
        _writer = writer;
        _treeDisplaySettings = treeDisplaySettings;
    }

    public void Visit(FileFileSystemComponent component)
    {
        AppendIndent();

        _buffer.Append(_treeDisplaySettings.FileSymbol);
        _buffer.Append(component.Name);
        _buffer.AppendLine();
    }

    public void Visit(DirectoryFileSystemComponent component)
    {
        AppendIndent();

        _buffer.Append(_treeDisplaySettings.DirectorySymbol);
        _buffer.Append(component.Name);
        _buffer.AppendLine();

        ++_currentIndentDepth;
        foreach (IFileSystemComponent child in component.Children)
        {
            child.Accept(this);
        }

        --_currentIndentDepth;
    }

    public void Flush()
    {
        _writer.Write(_buffer.ToString());
    }

    private void AppendIndent()
    {
        for (int i = 0; i < _currentIndentDepth; ++i)
        {
            _buffer.Append(_treeDisplaySettings.IndentSymbol);
        }
    }
}