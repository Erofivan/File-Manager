using Itmo.ObjectOrientedProgramming.Lab4.Core.OutputWriters;

namespace Itmo.ObjectOrientedProgramming.Lab4.Tests.Mocks;

public sealed class MockOutputWriter : IOutputWriter
{
    private readonly List<string> _lines = new List<string>();

    public IReadOnlyList<string> Lines => _lines;

    public string FullOutput => string.Join(string.Empty, _lines);

    public void Write(string content)
    {
        _lines.Add(content);
    }
}