using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization();
builder.Services.AddAuthentication().AddJwtBearer();

var app = builder.Build();

app.UseAuthorization();

app.MapGet("/", () => "Hello World!");

//JWT进行认证授权
//COokie 
// https://www.cnblogs.com/lludcmmcdull/p/17874175.html
app.MapGet("/secret", (ClaimsPrincipal user) => $"{user.Identities}")
    .RequireAuthorization();

app.Run();
