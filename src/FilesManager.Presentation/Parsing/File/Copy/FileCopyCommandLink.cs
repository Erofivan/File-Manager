using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Copy;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Copy;

public sealed class FileCopyCommandLink : FileSubCommandLinkBase
{
    public override CommandParseResult Handle(IEnumerator<string> tokensEnumerator)
    {
        if (tokensEnumerator.Current is not "copy")
            return CallNext(tokensEnumerator);

        if (tokensEnumerator.MoveNext() is false)
            return new CommandParseResult.Failure("Missing source path for 'file copy' command");

        string currentFilePath = tokensEnumerator.Current;

        if (tokensEnumerator.MoveNext() is false)
            return new CommandParseResult.Failure("Missing destination path for 'file copy' command");

        string newFilePath = tokensEnumerator.Current;

        FileCopyCommandBuilder builder = new FileCopyCommandBuilder()
            .WithCurrentFilePath(currentFilePath)
            .WithNewFilePath(newFilePath);

        return new CommandParseResult.Success(builder.Build());
    }
}
