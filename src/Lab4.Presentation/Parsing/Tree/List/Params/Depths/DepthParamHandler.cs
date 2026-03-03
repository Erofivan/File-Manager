using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Tree.List;
using System.Globalization;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree.List.Params.Depths;

public sealed class DepthParamHandler : TreeListParamHandlerBase
{
    protected override void Apply(IEnumerable<string> tokens, TreeListCommandBuilder builder)
    {
        string? depthValue = FindFlagValue(tokens, "-d");

        if (depthValue is not null
            && int.TryParse(depthValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out int depth))
        {
            builder.WithDepth(depth);
        }
    }
}
