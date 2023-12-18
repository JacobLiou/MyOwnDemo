using Quartz;
using Quartz.AspNetCore;
using QuartzDemo.Jobs;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
//builder.Services.AddQuartz();
//builder.Services.AddQuartzHostedService();
builder.Services.AddQuartz(q =>
{
    // base Quartz scheduler, job and trigger configuration
    var jobKey = new JobKey("HelloJobKey");
    q.AddJob<HelloJob>(opts => opts.WithIdentity(jobKey));
    q.AddTrigger(opts => opts.ForJob(jobKey).WithIdentity("HelloTrigger")
    //This Cron interval can be described as "run every minute" (when second is zero)
        .WithCronSchedule("0 * * ? * *"));

});

// ASP.NET Core hosting
builder.Services.AddQuartzServer(options =>
{
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
