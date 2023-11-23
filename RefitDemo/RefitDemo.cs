using Refit;

namespace Demo;

public interface IGithubApi
{
    [Get("/users/{user}")]
    Task<User> GetUser(string user);

    [Get("/users/{id}")]
    Task<User> GetUserById([AliasAs("id")]int userId);

    [Get("/users")]
    Task<List<User>> GetUsers();

    // [Get("/users")]
    // Task<List<User>> GetUsers(Pagination pagination);
    // // endpoint: /users?Page=1&Take=10 
    // // body: null

    // [Post("/users")]
    // Task<List<User>> GetUsers(Pagination pagination);
    // // url: /users 
    // // body: { page: 1, take: 10 }
    }

public record User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Username { get; set; }
}

public static class RefitDemo
{
    public static async Task CallRefit()
    {
        var userApi = RestService.For<IGithubApi>("https://jsonplaceholder.typicode.com");
        var users = await userApi.GetUsers();
        Console.WriteLine(String.Join(", ", users.Select(u => u.Name)));
    }
}