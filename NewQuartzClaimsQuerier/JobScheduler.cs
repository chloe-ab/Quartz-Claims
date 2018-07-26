using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewQuartzClaimsQuerier
{
    public class JobScheduler
    {
        public static void Start()
        {
            //construct a scheduler factory
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

            //get a scheduler:
            IScheduler scheduler = schedulerFactory.GetScheduler().Result; //added the result
            //scheduler.Start();

            //? TRYING OUT:
            JobKey jobKey1 = new JobKey("job1", "group1");
            //IJobDetail job1 = JobBuilder.newJob(job1.class).WithIdentity(jobKey1).Build();

            //define the job and tie it to Job class
            //IJobDetail job = JobBuilder.Create<Job>().WithIdentity("myJob", "group1").Build(); //the WithIdentity is optional
            IJobDetail job = JobBuilder.Create<Job>().WithIdentity(jobKey1).Build(); //the WithIdentity is optional


            //trigger the job to run now, and then every 40 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger", "group1")
                //my custom cron expression:  (cron is best suited, but other options to cron expression are: WithCalendarIntervalSchedule, WithDailyTimeIntervalSchedule, WithSimpleSchedule
                //.WithCronSchedule("0 0 8 ? * 2 *")  //0 seconds, 0 minutes, 8 a.m., no specific day of month, all months, Monday, all years
                 //cron expression JUST FOR TESTING:
                .WithCronSchedule("0 55 00 ? * 5")  //0 seconds, 0 minutes, midnight, no specific day of month, all months, Thurs
                .StartAt(DateTime.UtcNow)
                .WithPriority(1)
                .Build();

            //another example of triggering: start now and run every 40 seconds:
            //ITrigger trigger = TriggerBuilder.Create()
            //.WithIdentity("myTrigger", "group1")
            //.StartNow()
            //.WithSimpleSchedule(x => x
            //  .WithIntervalInSeconds(40)
            //  .RepeatForever())
            //.Build();

            scheduler.Start();

            //tell quartz to schedule the job using my trigger
            scheduler.ScheduleJob(job, trigger);
            //ADDED: DONT KNOW IF THIS SHOULD BE ABOVE OR BELOW:
            //scheduler.Start();

            Console.WriteLine("test");
            Console.ReadLine();

        }
    }
}
