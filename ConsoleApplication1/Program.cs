using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Web.Hosting;


using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();


            

        TwitterManagement tw = new TwitterManagement();
           
         var x= tw.GetTweetsAndPOSTAReportToPincident().GetAwaiter().GetResult();

            Thread.Sleep(1000 * 60 * 3);

            stopWatch.Stop();

            

            TimeSpan ts = stopWatch.Elapsed;


            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);

           
            Console.WriteLine("RunTimeTrigger " + elapsedTime);
           

        }
    }
}
