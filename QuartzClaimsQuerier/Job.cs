using Quartz;
using System;
using System.Threading.Tasks;

namespace NewQuartzClaimsQuerier
{
    public class Job : IJob  //custom job class
    {          
        async Task IJob.Execute(IJobExecutionContext context)
        {
            //the job to be performed:
            //Console.WriteLine("job beginning execution");
            while (true)
            {
                Processor.Run();
                await Console.Out.WriteLineAsync("job completed");
            }

        }
    }
}

