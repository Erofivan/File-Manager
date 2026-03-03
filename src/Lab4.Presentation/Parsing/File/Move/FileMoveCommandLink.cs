using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Move;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Move;

public sealed class FileMoveCommandLink : FileSubCommandLinkBase
{
    public override CommandParseResult Handle(IEnumerator<string> tokens)
    {
        if (tokens.Current is not "move")
            return CallNext(tokens);

        if (!tokens.MoveNext())
            return new CommandParseResult.Failure("Missing source path for 'file move' command");

        string sourcePath = tokens.Current;

        if (!tokens.MoveNext())
            return new CommandParseResult.Failure("Missing destination path for 'file move' command");

        string destinationPath = tokens.Current;

        FileMoveCommandBuilder builder = new FileMoveCommandBuilder()
            .WithCurrentFilePath(sourcePath)
            .WithNewFilePath(destinationPath);

        return new CommandParseResult.Success(builder.Build());
    }
}
