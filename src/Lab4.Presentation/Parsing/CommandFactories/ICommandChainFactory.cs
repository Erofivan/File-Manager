namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing.CommandFactories;

public interface ICommandChainFactory
{
    ICommandLink Create();
}