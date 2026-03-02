using Itmo.ObjectOrientedProgramming.Lab4.Core.OutputWriters;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation;

public sealed class ConsoleOutputWriter : IOutputWriter
{
    public void Write(string content)
    {
        Console.WriteLine(content);
    }
}