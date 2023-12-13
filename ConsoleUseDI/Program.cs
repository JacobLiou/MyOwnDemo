// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World!");

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