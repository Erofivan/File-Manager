using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.Core.OutputWriters;

namespace Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;

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
        if (context.FileSystem.IsConnected is false)
            return new CommandExecutionResult.FileSystemNotConnected();

        string resolvedPath = context.ResolvePath(_path);

        if (context.FileSystem.FileExists(resolvedPath) is false)
            return new CommandExecutionResult.FileNotFound(_path);

        FileReadResult readOperationResult = context.FileSystem.ReadFile(resolvedPath);

        switch (readOperationResult)
        {
            case FileReadResult.Failure failure:
                return new CommandExecutionResult.Failure(failure.Message);

            case FileReadResult.Success success:
                _outputWriter.Write(success.Content);
                break;

            default:
                throw new InvalidOperationException("Unexpected file read result.");
        }

        return new CommandExecutionResult.Success();
    }
}