using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Move;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Move;

public sealed class FileMoveCommandLink : FileSubCommandLinkBase
{
    public override CommandParseResult Handle(IEnumerator<string> tokensEnumerator)
    {
        if (tokensEnumerator.Current is not "move")
            return CallNext(tokensEnumerator);

        if (tokensEnumerator.MoveNext() is false)
            return new CommandParseResult.Failure("Missing source path for 'file move' command");

        string currentFilePath = tokensEnumerator.Current;

        if (tokensEnumerator.MoveNext() is false)
            return new CommandParseResult.Failure("Missing destination path for 'file move' command");

        string newFilePath = tokensEnumerator.Current;

        FileMoveCommandBuilder builder = new FileMoveCommandBuilder()
            .WithCurrentFilePath(currentFilePath)
            .WithNewFilePath(newFilePath);

        return new CommandParseResult.Success(builder.Build());
    }
}
