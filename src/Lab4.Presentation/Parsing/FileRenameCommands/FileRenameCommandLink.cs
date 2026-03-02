using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.FileRenameCommands;

public sealed class FileRenameCommandLink : CommandLinkBase
{
    public override ICommand? Handle(string[] args)
    {
        if (args.Length < 4 || args[0] is not "file" || args[1] is not "rename")
            return CallNext(args);

        FileRenameCommandBuilder builder = new FileRenameCommandBuilder()
            .WithFilePath(args[2])
            .WithFilePath(args[3]);

        return builder.Build();
    }
}