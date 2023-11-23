using LiteDB;
using LiteDBDemo;

// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

//LiteDB 是轻量级的MongoDB  采用JSON NoSqli形式保存配置
//Sqlite LiteDb都可以用来做本地配置库



using var db = new LiteDatabase(Path.Combine(Directory.GetCurrentDirectory(), "Mydata.db"));
var col = db.GetCollection<Customer>("customers");

var cus = new Customer
{
    Name = "John Dee",
    Phones = ["8000-0000", "9000-0000"],
    IsActive = true,
};
col.Insert(cus);

cus.Name = "New Name";
col.Update(cus);

//Index document 
col.EnsureIndex(x => x.Name);

//查询
var results = col.Query().Where(x => x.Name.StartsWith("N")).
    OrderBy(x => x.Name).ToList();
Console.WriteLine(results);