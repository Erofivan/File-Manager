using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using System.Globalization;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.TreeListCommands;

public sealed class TreeListCommandLink : CommandLinkBase
{
    public override ICommand? Handle(string[] args)
    {
        if (args.Length < 2 || args[0] is not "tree" || args[1] is not "list")
            return CallNext(args);

        var builder = new TreeListCommandBuilder();

        string? depthValue = FindFlag(args, "-d");

        if (depthValue is not null
            && int.TryParse(depthValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out int depth))
        {
            builder.WithDepth(depth);
        }

        return builder.Build();
    }
}