//using System.Text.Json;

using Newtonsoft.Json;
using System.Dynamic;
using System.Threading.Tasks;
using System.Threading.Channels;
using Demo;
//dynamic obj = JsonConvert.DeserializeObject());

Jsondemo jsondemo = new Jsondemo
{
    MyProperty = 1,
    MyProperty1 = "",
    Nest1 = new Nest1()
    {
        Nest2s = new List<Nest2>{
            new Nest2 (){ MyBool = true, }
        },
    }
};

File.WriteAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "haha.json"), JsonConvert.SerializeObject(jsondemo));

await RefitDemo.CallRefit();
RSADemo.CallDemo();

Person.EnumerateFilesRecursively(AppDomain.CurrentDomain.BaseDirectory);
Person.TraverseDirectory(AppContext.BaseDirectory, ".dll");

dynamic dyn1 = new ExpandoObject();
dyn1.Name = "Haha";
Console.WriteLine(dyn1.Name);

dyn1.Describe = (Func<string>)(() =>
{
    return $"My name is {dyn1.Name}";
});

Console.WriteLine(dyn1.Describe());

var jsonString = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "response.json"));

var dynamicObject = JsonConvert.DeserializeObject<dynamic>(jsonString)!;
Console.WriteLine(dynamicObject.code);

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// AsyncLocal<Person> context = new AsyncLocal<Person>();
// context.Value = new Person { Id = 1, Name = "张三" };
// Console.WriteLine($"Main之前:{context.Value.Name},ThreadId={Thread.CurrentThread.ManagedThreadId}");
// await Task.Run(() =>
// {
//     Console.WriteLine($"Task1之前:{context.Value.Name},ThreadId={Thread.CurrentThread.ManagedThreadId}");
//     context.Value.Name = "李四";
//     Console.WriteLine($"Task1之后:{context.Value.Name},ThreadId={Thread.CurrentThread.ManagedThreadId}");
// });

// await Task.Run(() =>
// {
//     Console.WriteLine($"Task2之前:{context.Value.Name},ThreadId={Thread.CurrentThread.ManagedThreadId}");
//     context.Value.Name = "王五";
//     Console.WriteLine($"Task2之后:{context.Value.Name},ThreadId={Thread.CurrentThread.ManagedThreadId}");
// });
// Console.WriteLine($"Main之后:{context.Value.Name},ThreadId={Thread.CurrentThread.ManagedThreadId}");

var context = new AsyncLocal<Person>();
context.Value = new Person { Id = 1, Name = "张三" };
Console.WriteLine($"Main之前:{context.Value.Name},ThreadId={Thread.CurrentThread.ManagedThreadId}");
await Task.Run(() =>
{
    Console.WriteLine($"Task1之前:{context.Value.Name},ThreadId={Thread.CurrentThread.ManagedThreadId}");
    context.Value = new Person { Id = 2, Name = "李四" };
    Console.WriteLine($"Task1之后:{context.Value.Name},ThreadId={Thread.CurrentThread.ManagedThreadId}");
});

await Task.Run(() =>
{
    Console.WriteLine($"Task2之前:{context.Value.Name},ThreadId={Thread.CurrentThread.ManagedThreadId}");
    context.Value = new Person { Id = 3, Name = "王五" }; ;
    Console.WriteLine($"Task2之后:{context.Value.Name},ThreadId={Thread.CurrentThread.ManagedThreadId}");
});
Console.WriteLine($"Main之后:{context.Value.Name},ThreadId={Thread.CurrentThread.ManagedThreadId}");




public class Person
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public static ChannelReader<string> EnumerateFilesRecursively(string root, int capacity = 100, CancellationToken token = default)
    {
        var output = Channel.CreateBounded<string>(capacity);

        async Task WalkDir(string path)
        {
            IEnumerable<string> files = null, directories = null;
            try
            {
                files = Directory.EnumerateFiles(path);
                directories = Directory.EnumerateDirectories(path);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            if (files != null)
            {
                foreach (var file in files)
                {
                    await output.Writer.WriteAsync(file, token);
                }
            }

            if (directories != null)
                await Task.WhenAll(directories.Select(WalkDir));
        }

        Task.Run(async () =>
        {
            await WalkDir(root);
            output.Writer.Complete();
        }, token);

        return output.Reader;
    }

    public static void TraverseDirectory(string root, string extension)
    {
        var stack = new Stack<string>();
        stack.Push(root);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            var files = Directory.GetFiles(current, "*" + extension);

            foreach (var file in files)
            {
                Console.WriteLine(file);
            }

            var folders = Directory.GetDirectories(current);

            foreach (var folder in folders)
            {
                stack.Push(folder);
            }
        }
    }
}


public class Jsondemo
{
    public int MyProperty { get; set; }

    public string MyProperty1 { get; set; }

    public Nest1 Nest1 { get; set; }
}

public class Nest1
{
    public object[] now { get; set; }

    public IList<Nest2> Nest2s { get; set; }
}

public record Nest2
{
    public bool MyBool { get; set; }
}


