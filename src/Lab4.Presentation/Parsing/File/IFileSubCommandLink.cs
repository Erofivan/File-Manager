namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File;

public interface IFileSubCommandLink
{
    IFileSubCommandLink AddNext(IFileSubCommandLink link);

    CommandParseResult Handle(IEnumerator<string> tokens);
}
