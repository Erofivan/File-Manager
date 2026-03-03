using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Rename;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Rename;

public sealed class FileRenameCommandLink : FileSubCommandLinkBase
{
    public override CommandParseResult Handle(IEnumerator<string> tokens)
    {
        if (tokens.Current is not "rename")
            return CallNext(tokens);

        if (!tokens.MoveNext())
            return new CommandParseResult.Failure("Missing path for 'file rename' command");

        string path = tokens.Current;

        if (!tokens.MoveNext())
            return new CommandParseResult.Failure("Missing new name for 'file rename' command");

        string newName = tokens.Current;

        FileRenameCommandBuilder builder = new FileRenameCommandBuilder()
            .WithFilePath(path)
            .WithNewFileName(newName);

        return new CommandParseResult.Success(builder.Build());
    }
}
