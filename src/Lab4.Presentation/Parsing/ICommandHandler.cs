namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing;

public interface ICommandHandler
{
    CommandParseResult Handle(IEnumerable<string> tokens);
}