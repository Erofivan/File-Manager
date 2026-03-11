using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Rename;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.File.Rename;

public sealed class FileRenameCommandLink : FileSubCommandLinkBase
{
    public override CommandParseResult Handle(IEnumerator<string> tokensEnumerator)
    {
        if (tokensEnumerator.Current is not "rename")
            return CallNext(tokensEnumerator);

        if (tokensEnumerator.MoveNext() is false)
            return new CommandParseResult.Failure("Missing path for 'file rename' command");

        string path = tokensEnumerator.Current;

        if (tokensEnumerator.MoveNext() is false)
            return new CommandParseResult.Failure("Missing new name for 'file rename' command");

        string newName = tokensEnumerator.Current;

        FileRenameCommandBuilder builder = new FileRenameCommandBuilder()
            .WithFilePath(path)
            .WithNewFileName(newName);

        return new CommandParseResult.Success(builder.Build());
    }
}
