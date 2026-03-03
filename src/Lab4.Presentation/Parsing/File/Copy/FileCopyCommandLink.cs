using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Copy;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Copy;

public sealed class FileCopyCommandLink : FileSubCommandLinkBase
{
    public override CommandParseResult Handle(IEnumerator<string> tokens)
    {
        if (tokens.Current is not "copy")
            return CallNext(tokens);

        if (!tokens.MoveNext())
            return new CommandParseResult.Failure("Missing source path for 'file copy' command");

        string sourcePath = tokens.Current;

        if (!tokens.MoveNext())
            return new CommandParseResult.Failure("Missing destination path for 'file copy' command");

        string destinationPath = tokens.Current;

        FileCopyCommandBuilder builder = new FileCopyCommandBuilder()
            .WithCurrentFilePath(sourcePath)
            .WithNewFilePath(destinationPath);

        return new CommandParseResult.Success(builder.Build());
    }
}
