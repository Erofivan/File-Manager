using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.FileCopyCommands;

public sealed class FileCopyCommandLink : CommandLinkBase
{
    public override ICommand? Handle(string[] args)
    {
        if (args.Length < 4 || args[0] is not "file" || args[1] is not "copy")
            return CallNext(args);

        FileCopyCommandBuilder builder = new FileCopyCommandBuilder()
            .WithCurrentFilePath(args[2])
            .WithNewFilePath(args[3]);

        return builder.Build();
    }
}