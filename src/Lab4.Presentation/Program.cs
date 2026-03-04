using Itmo.ObjectOrientedProgramming.Lab4.Core;
using Itmo.ObjectOrientedProgramming.Lab4.Core.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Core.FileSystems.FileSystemDisplayers;
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
            " 🪆 : ",
            "\t");

        var executionContext = new Context();

        var settings = new CommandHandlerSettings(outputWriter, fileSystemTreeDisplaySettings);
        ICommandHandler commandHandlerChain = new CommandHandlerFactory(settings).Create();

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
            CommandParseResult parseResult = commandHandlerChain.Handle(commandTokens);

            if (parseResult is CommandParseResult.Failure parseFailure)
            {
                Console.WriteLine($"Parse error: {parseFailure.Message}");
                continue;
            }

            if (parseResult is not CommandParseResult.Success success)
                throw new UnreachableException();

            CommandExecutionResult executionResult = success.Command.Execute(executionContext);
            PrintExecutionResult(executionResult);
        }

        Console.WriteLine($"The program has finished!");
    }

    private static void PrintExecutionResult(CommandExecutionResult result)
    {
        switch (result)
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

    private static string PersistentReadLine(List<string> history)
    {
        var buffer = new StringBuilder();
        int historyIndex = history.Count;
        int cursorPosition = 0;

        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    Console.WriteLine();
                    return buffer.ToString();

                case ConsoleKey.Backspace:
                    if (cursorPosition > 0)
                    {
                        buffer.Remove(cursorPosition - 1, 1);
                        cursorPosition--;
                        RedrawLine(buffer, cursorPosition);
                    }

                    break;

                case ConsoleKey.LeftArrow:
                    if (cursorPosition > 0)
                    {
                        cursorPosition--;
                        Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    }

                    break;

                case ConsoleKey.RightArrow:
                    if (cursorPosition < buffer.Length)
                    {
                        cursorPosition++;
                        Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                    }

                    break;

                case ConsoleKey.UpArrow:
                    if (history.Count == 0) break;

                    historyIndex = Math.Max(0, historyIndex - 1);
                    cursorPosition = ReplaceCurrentLine(history[historyIndex], buffer);
                    break;

                case ConsoleKey.DownArrow:
                    if (history.Count == 0) break;

                    historyIndex = Math.Min(history.Count, historyIndex + 1);

                    string text = historyIndex < history.Count
                        ? history[historyIndex]
                        : string.Empty;

                    cursorPosition = ReplaceCurrentLine(text, buffer);
                    break;

                default:
                    if (!char.IsControl(key.KeyChar))
                    {
                        buffer.Insert(cursorPosition, key.KeyChar);
                        cursorPosition++;
                        RedrawLine(buffer, cursorPosition);
                    }

                    break;
            }
        }
    }

    private static int ReplaceCurrentLine(string text, StringBuilder buffer)
    {
        buffer.Clear();
        buffer.Append(text);
        RedrawLine(buffer, buffer.Length);
        return buffer.Length;
    }

    private static void RedrawLine(StringBuilder buffer, int cursorPosition)
    {
        Console.Write("\r> ");
        Console.Write(new string(' ', Console.BufferWidth - 2));
        Console.Write("\r> ");
        Console.Write(buffer.ToString());

        Console.SetCursorPosition(2 + cursorPosition, Console.CursorTop);
    }
}