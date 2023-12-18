// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");
Console.SetCursorPosition(0, 1);
Console.BackgroundColor = System.ConsoleColor.Red;
Console.Write("Boom");

Console.CursorVisible = true;
Console.ReadKey();
Console.ResetColor();

// Get an array with the values of ConsoleColor enumeration members.
ConsoleColor[] colors = (ConsoleColor[])ConsoleColor.GetValues(typeof(ConsoleColor));
// Save the current background and foreground colors.
ConsoleColor currentBackground = Console.BackgroundColor;
ConsoleColor currentForeground = Console.ForegroundColor;

// Display all foreground colors except the one that matches the background.
Console.WriteLine("All the foreground colors except {0}, the background color:",
                currentBackground);
foreach (var color in colors)
{
    if (color == currentBackground) continue;

    Console.ForegroundColor = color;
    Console.WriteLine("   The foreground color is {0}.", color);
}
Console.WriteLine();
// Restore the foreground color.
Console.ForegroundColor = currentForeground;

// Display each background color except the one that matches the current foreground color.
Console.WriteLine("All the background colors except {0}, the foreground color:",
                currentForeground);
foreach (var color in colors)
{
    if (color == currentForeground) continue;

    Console.BackgroundColor = color;
    Console.WriteLine("   The background color is {0}.", color);
}

// Restore the original console colors.
Console.ResetColor();
Console.WriteLine("\nOriginal colors restored...");

ServiceCollection serviceCollection = new ServiceCollection();
serviceCollection.AddLogging((loggingBuilder) => loggingBuilder.
    SetMinimumLevel(LogLevel.Trace).AddConsole());
serviceCollection.AddKeyedScoped<IHello, GoodHello>("IGoodHello");
serviceCollection.AddKeyedScoped<IHello, BadHello>("IBadHello");

var serviceProvider = serviceCollection.BuildServiceProvider();

//serviceProvider.GetService<ILoggerFactory>().AddConsole();
var goodHello = serviceProvider.GetKeyedService<IHello>("IGoodHello");
goodHello?.SayHello();
var badHello = serviceProvider.GetKeyedService<IHello>("IBadHello");
badHello?.SayHello();


internal class GoodHello(ILogger<GoodHello> logger) : IHello
{
    public ILogger<GoodHello> Logger { get; } = logger;

    public string SayHello()
    {
        Logger.LogInformation($"Hello");
        //Console.WriteLine("Good Hello");
        return "Good Hello";
    }
}

internal class BadHello(ILogger<BadHello> logger) : IHello
{
    public ILogger<BadHello> Logger { get; } = logger;

    public string SayHello()
    {
        Logger.LogInformation($"Hello");
        Console.WriteLine("Bad Hello");
        return "Bad Hello";
    }
}

internal interface IHello
{
    string SayHello();
}