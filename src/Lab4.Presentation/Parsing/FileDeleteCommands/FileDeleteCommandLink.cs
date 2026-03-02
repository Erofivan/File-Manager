using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.FileDeleteCommands;

public sealed class FileDeleteCommandLink : CommandLinkBase
{
    public override ICommand? Handle(string[] args)
    {
        if (args.Length < 3 || args[0] is not "file" || args[1] is not "delete")
            return CallNext(args);

        FileDeleteCommandBuilder builder = new FileDeleteCommandBuilder()
            .WithPath(args[2]);

        return builder.Build();
    }
}