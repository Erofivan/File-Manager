using Itmo.ObjectOrientedProgramming.Lab4.Core;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.Trees;
using Itmo.ObjectOrientedProgramming.Lab4.Presentation.Parsing;
using System.Diagnostics;
using System.Text;

namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation;

public static class Program
{
    public static void Main(string[] args)
    {
        var outputWriter = new ConsoleOutputWriter();
        var fileSystemTreeDisplaySettings = new FileSystemTreeDisplaySettings(
            "🇷🇺: ",
            "🪆: ",
            " ↳");

        var executionContext = new Context(outputWriter, fileSystemTreeDisplaySettings);
        ICommandHandler commandHandlerChain = new CommandHandlerFactory().Create();

        var commandHistory = new List<string>();

        Console.WriteLine($"The program has started!");
        Console.WriteLine($"Type 'exit' to quit");

        while (true)
        {
            Console.Write("> ");
            string input = PersistentReadLine(commandHistory);

            if (string.IsNullOrWhiteSpace(input))
                continue;

            if (input is "exit")
                break;

            commandHistory.Add(input);

            string[] commandTokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            ICommand? command = commandHandlerChain.Handle(commandTokens);

            if (command is null)
            {
                Console.WriteLine("Unknown command");
                continue;
            }

            CommandExecutionResult executionResult = command.Execute(executionContext);

            switch (executionResult)
            {
                case CommandExecutionResult.Success:
                    break;
                case CommandExecutionResult.FileSystemNotConnected:
                    Console.WriteLine("Error: file system is not connected");
                    break;
                case CommandExecutionResult.FileNotFound fileNotFound:
                    Console.WriteLine($"Error: file not found: {fileNotFound.Path}");
                    break;
                case CommandExecutionResult.DirectoryNotFound directoryNotFound:
                    Console.WriteLine($"Error: directory not found: {directoryNotFound.Path}");
                    break;
                case CommandExecutionResult.Failure failure:
                    Console.WriteLine($"Error: {failure.Message}");
                    break;
                default:
                    throw new UnreachableException();
            }
        }

        Console.WriteLine($"The program has finished!");
    }

    private static string PersistentReadLine(List<string> history)
    {
        var buffer = new StringBuilder();
        int historyIndex = history.Count;

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    Console.WriteLine();
                    return buffer.ToString();

                case ConsoleKey.Backspace:
                    if (buffer.Length > 0)
                    {
                        buffer.Remove(buffer.Length - 1, 1);
                        Console.Write("\b \b");
                    }

                    break;

                case ConsoleKey.UpArrow:
                    if (history.Count is 0)
                        break;

                    historyIndex = Math.Max(0, historyIndex - 1);
                    ReplaceCurrentLine(history[historyIndex], buffer);
                    break;

                case ConsoleKey.DownArrow:
                    if (history.Count is 0)
                        break;

                    historyIndex = Math.Min(history.Count, historyIndex + 1);

                    string text = historyIndex < history.Count
                        ? history[historyIndex]
                        : string.Empty;

                    ReplaceCurrentLine(text, buffer);
                    break;

                default:
                    if (char.IsControl(key.KeyChar) is false)
                    {
                        buffer.Append(key.KeyChar);
                        Console.Write(key.KeyChar);
                    }

                    break;
            }
        }
    }

    private static void ReplaceCurrentLine(string text, StringBuilder buffer)
    {
        Console.Write("\r> " + new string(' ', buffer.Length));
        Console.Write("\r> ");

        buffer.Clear();
        buffer.Append(text);
        Console.Write(text);
    }
}