// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using Microsoft.Extensions.DependencyInjection;
using Serilog;

var serviceCollection = new ServiceCollection();
serviceCollection.AddLogging(logBuilder => logBuilder.AddSerilog());


