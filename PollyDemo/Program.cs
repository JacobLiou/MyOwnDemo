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


var policy = Policy.Handle<HttpIOException>()
    .OrResult<HttpResponseMessage>(result => result.StatusCode == System.Net.HttpStatusCode.InternalServerError)
    .WaitAndRetry(3, _ => TimeSpan.FromSeconds(1));


ISyncPolicy policy1 = Policy.RateLimit(numberOfExecutions:100, perTimeSpan:TimeSpan.FromMinutes(1));
policy1.Execute(() => {

});


var policy2 = Policy.Bulkhead(
    maxParallelization: 100,
    maxQueuingActions:50
);


ResiliencePipeline pipeline1 = new ResiliencePipelineBuilder()
.AddTimeout(TimeSpan.FromSeconds(10))
.Build();

var policy3 = Policy.Handle<HttpIOException>()
.CircuitBreaker(
    exceptionsAllowedBeforeBreaking: 2,
    durationOfBreak: TimeSpan.FromSeconds(2)
);


Context context = new Context();
context["key1"] = "Value1";


string val1 = (string)context["key1"];


//V8 创建上下文
ResilienceContext context1 = ResilienceContextPool.Shared.Get();

ResiliencePropertyKey<string> key1 = new();
context1.Properties.Set(key1, "Value1");
