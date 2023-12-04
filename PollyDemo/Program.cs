// See https://aka.ms/new-console-template for more information
using Polly;

// Console.WriteLine("Hello, World!");
int i = 0;

//V7写法
var syncPolicy = Policy.Handle<Exception>()
.WaitAndRetry(3, _ => TimeSpan.FromSeconds(1));

syncPolicy.Execute(() =>
{
    i++;
    if(i < 2) throw new Exception("Purpose");
    Console.WriteLine("Sucessfully  completed");

});

//V8全新设计和API 采用pipeline和Strategy
ResiliencePipeline pipeline = new ResiliencePipelineBuilder().
AddRetry(new Polly.Retry.RetryStrategyOptions
{
    ShouldHandle = new PredicateBuilder().Handle<Exception>(),
    Delay = TimeSpan.FromSeconds(1),
    MaxRetryAttempts = 3,
    BackoffType = DelayBackoffType.Constant
})
.Build();

pipeline.Execute(() => {
    i++;
    if(i < 2) throw new Exception("Purpose");
    Console.WriteLine("Sucessfully  completed");
});

var cancellactionToken = new CancellationToken();
await pipeline.ExecuteAsync(async url => 
{
    var httpClient = new HttpClient();
    await httpClient.GetAsync("www.google.com", cancellactionToken)
}, cancellactionToken);