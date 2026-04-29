namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File;

public interface IFileSubCommandHandler
{
    CommandParseResult Handle(IEnumerator<string> tokensEnumerator);
}

public interface IFileSubCommandLink : IFileSubCommandHandler
{
    IFileSubCommandLink AddNext(IFileSubCommandLink link);
}