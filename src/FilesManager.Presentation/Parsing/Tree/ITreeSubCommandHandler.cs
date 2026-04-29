namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree;

public interface ITreeSubCommandHandler
{
    CommandParseResult Handle(IEnumerator<string> tokensEnumerator);
}

public interface ITreeSubCommandLink : ITreeSubCommandHandler
{
    ITreeSubCommandLink AddNext(ITreeSubCommandLink link);
}
