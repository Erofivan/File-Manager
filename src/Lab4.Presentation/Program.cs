namespace Itmo.ObjectOrientedProgramming.Lab4.Presentation;

public static class Program
{
    public static void Main(string[] args)
    {
        var outputWriter = new ConsoleOutputWriter();

        Console.WriteLine($"The program has started!");
        Console.WriteLine($"Type 'exit' to quit");

        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                continue;

            if (input == "exit")
                break;

            string[] commandTokens = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // TODO: finish the lab work
        }

        Console.WriteLine($"The program has finished!");
    }
}