namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing;

public interface ICommandHandler
{
    CommandParseResult Handle(IEnumerator<string> tokensEnumerator);
}

public interface ICommandLink : ICommandHandler
{
    ICommandLink AddNext(ICommandLink link);
}