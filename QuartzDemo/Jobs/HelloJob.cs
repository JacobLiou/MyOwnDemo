using Quartz;

namespace QuartzDemo.Jobs
{
    public class HelloJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Hello, JOb executed");
            return Task.CompletedTask;
        }
    }
}
