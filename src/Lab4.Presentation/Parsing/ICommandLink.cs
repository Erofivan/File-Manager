namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing;

public interface ICommandLink
{
    ICommandLink AddNext(ICommandLink link);

    CommandParseResult Handle(IEnumerator<string> tokens);
}