using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.Tree.List;
using System.Globalization;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.Tree.List.Params.Depths;

public sealed class DepthParamHandler : TreeListParamHandlerBase
{
    public override CommandParseResult Handle(IEnumerator<string> tokensEnumerator, TreeListCommandBuilder builder)
    {
        if (tokensEnumerator.Current is not "-d")
            return CallNext(tokensEnumerator, builder);

        if (tokensEnumerator.MoveNext() is false)
        {
            return new CommandParseResult.Failure("-d flag missing value");
        }

        if (int.TryParse(tokensEnumerator.Current, NumberStyles.Integer, CultureInfo.InvariantCulture, out int depth))
            builder.WithDepth(depth);

        return new CommandParseResult.Success(builder.Build());
    }
}
