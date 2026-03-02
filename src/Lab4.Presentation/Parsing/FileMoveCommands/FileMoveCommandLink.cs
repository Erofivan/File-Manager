using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.FileMoveCommands;

public sealed class FileMoveCommandLink : CommandLinkBase
{
    public override ICommand? Handle(string[] args)
    {
        if (args.Length < 4 || args[0] is not "file" || args[1] is not "move")
            return CallNext(args);

        FileMoveCommandBuilder builder = new FileMoveCommandBuilder()
            .WithCurrentFilePath(args[2])
            .WithNewFilePath(args[3]);

        return builder.Build();
    }
}