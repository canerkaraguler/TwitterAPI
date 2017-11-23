using System;
using System.Text;

using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;



using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Configuration;

namespace ConsoleApplication1
{
    public static class HyperConfig
    {
        //HPSİ AYNI FORMATTA OLSUN.++++++++++++
        public static string consumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
        public static string consumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];
        public static string categoryFire = ConfigurationManager.AppSettings["Fire"];
        public static string categoryInfrastructure = ConfigurationManager.AppSettings["Infrastructure"];
        public static string categoryTraffic = ConfigurationManager.AppSettings["Traffic"];
        public static string categoryNaturalDisaster = ConfigurationManager.AppSettings["NaturalDisaster"];
        public static string categorySecurity = ConfigurationManager.AppSettings["Security"];
        public static string bearerKey = ConfigurationManager.AppSettings["Bearer"];
        public static string logStatus1 = ConfigurationManager.AppSettings["LogStatus1"];
        public static string logStatus2 = ConfigurationManager.AppSettings["LogStatus2"];
        public static string logStatus3 = ConfigurationManager.AppSettings["LogStatus3"];
        public static string tweetsJsonFormat = ConfigurationManager.AppSettings["TweetsJsonFormat"];
        public static string postReportBaseUrl = ConfigurationManager.AppSettings["POSTReportBaseURL"];
        public static string tweets = ConfigurationManager.AppSettings["Tweets"];
        public static string tweetId = ConfigurationManager.AppSettings["TweetId"];
        public static string ilIlce = ConfigurationManager.AppSettings["IlIlce"];
        public static string limit = ConfigurationManager.AppSettings["Limit"];
    }
}
