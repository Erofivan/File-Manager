namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree;

public interface ITreeSubCommandLink
{
    ITreeSubCommandLink AddNext(ITreeSubCommandLink link);

    CommandParseResult Handle(IEnumerator<string> tokens);
}