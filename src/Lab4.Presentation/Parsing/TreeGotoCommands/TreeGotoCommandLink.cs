using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.TreeGotoCommands;

public sealed class TreeGotoCommandLink : CommandLinkBase
{
    public override ICommand? Handle(string[] args)
    {
        if (args.Length < 3 || args[0] is not "tree" || args[1] is not "goto")
            return CallNext(args);

        TreeGotoCommandBuilder builder = new TreeGotoCommandBuilder()
            .WithPath(args[2]);

        return builder.Build();
    }
}