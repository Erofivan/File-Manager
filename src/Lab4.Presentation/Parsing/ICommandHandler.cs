using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing;

public interface ICommandHandler
{
    ICommand? Handle(string[] args);
}

public interface ICommandLink : ICommandHandler
{
    ICommandLink AddNext(ICommandLink link);
}