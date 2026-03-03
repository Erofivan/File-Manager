using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.FileSystemOperationsResults;
using Itmo.ObjectOrientedProgramming.Lab4.Core.OutputWriters;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands.File.Show;

public sealed class FileShowCommand : ICommand
{
    private readonly string _path;
    private readonly IOutputWriter _outputWriter;

    public FileShowCommand(string path, IOutputWriter outputWriter)
    {
        _path = path;
        _outputWriter = outputWriter;
    }

    public CommandExecutionResult Execute(Context context)
    {
        if (context.IsConnected is false)
            return new CommandExecutionResult.FileSystemNotConnected();

        string resolvedPath = context.ResolvePath(_path);

        if (context.FileSystem.FileExists(resolvedPath) is false)
            return new CommandExecutionResult.FileNotFound(_path);

        FileReadOperationResult readResult = context.FileSystem.ReadFile(resolvedPath);

        return readResult switch
        {
            FileReadOperationResult.Failure failure => new CommandExecutionResult.Failure(failure.Message),
            FileReadOperationResult.Success success => WriteAndSucceed(success.Content),
            _ => throw new InvalidOperationException("Unexpected file read result."),
        };
    }

    private CommandExecutionResult.Success WriteAndSucceed(string content)
    {
        _outputWriter.Write(content);

        return new CommandExecutionResult.Success();
    }
}