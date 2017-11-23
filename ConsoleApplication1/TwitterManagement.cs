using System;
using System.Text;

using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Configuration;
using System.Collections.Generic;


namespace ConsoleApplication1
{

    
    public class TwitterManagement
    {

        
        public async Task<String> GetTweetsAndPOSTAReportToPincident()
        {



            //https://dev.twitter.com/rest/reference/get/search/tweets adresinde resource information adı altında  15 dk ıçerisinde 450 request yollayabıleceğimiz yazıyor.
            //maximum count/rpp ( rows per page ) sayısı 100 olduğu için 15 dk içerisinde maximum 45,000 tweet cekebiliriz.
            //https://dev.twitter.com/rest/public/search adresinde best practices adı altında 10 keyword ve operatorden fazla kullanılmaması oneriliyor.





            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();



           


            string text = File.ReadAllText(@HyperConfig.ilIlce, Encoding.GetEncoding("iso-8859-9"));

           


           

            dynamic cityCounty = JsonConvert.DeserializeObject<dynamic>(text);




          

            //Filelar Debug klasoru içinde tutuluyor !!!
            
          /*  StreamWriter strTweetsJsonFormat = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), HyperConfig.tweetsJsonFormat), append: true );
           
                StreamWriter strTweets = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), HyperConfig.tweets), append: true);
                StreamWriter strTweetId = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), HyperConfig.tweetId), append: true);
                
            */

            TweetFilter tweetFilter = new TweetFilter();
            

            Class.TwitterRequest twitterRequestObject = new Class.TwitterRequest();
            Class.PincidentRequest pincidentRequestObject = new Class.PincidentRequest();





            String[] screenNames = new string[5] { "@ntv", "@cnnturk", "@Hurriyet", "@gazetesozcu", "@yeniasircomtr" };
            
            String[] categories = new String[5] { HyperConfig.categoryFire, HyperConfig.categoryInfrastructure, HyperConfig.categoryNaturalDisaster, HyperConfig.categorySecurity, HyperConfig.categoryTraffic };
            var counter = 0;



            var filteredTweetArray = new JArray();
          
            var tweetArrayJsonFormat = new JArray();
            var tweetIdArrayJsonFormat = new JArray();




            


            int d = 0;
            

            foreach (var screenN in screenNames)
            {
               

                foreach (var category in categories)
                {
                    

                    dynamic tweetsFromRequest = JsonConvert.DeserializeObject<dynamic>(await twitterRequestObject.GetTweets(category,  screenN));
                    
                    tweetArrayJsonFormat.Add(tweetsFromRequest);

                   
                    foreach (var item in tweetsFromRequest.statuses)
                    {
                        
                        if (item.coordinates == null)
                        {
                            
                            for (int i = 0; i < 917; i++)
                            {


                                d++;
                                Class.CriteriaObject.TweetFilterCriteriaObject criteriaObject = new Class.CriteriaObject.TweetFilterCriteriaObject();
                                criteriaObject.city = cityCounty[i].il.ToString();
                                criteriaObject.county = cityCounty[i].ilce.ToString();
                                criteriaObject.item = item;
                                criteriaObject.category = category;
                                criteriaObject.allId = tweetIdArrayJsonFormat;
                                criteriaObject.withCoordinate = false;

                                var filteredTweet = tweetFilter.Filter(criteriaObject);




                                if (filteredTweet != null)
                                {
                                   if( await pincidentRequestObject.POSTReport(filteredTweet) == null)
                                    {
                                        return null;
                                    }
                                  
                                    filteredTweetArray.Add(filteredTweet);

                                    

                                }



                            }


                            counter++;

                           
                        }


                        else
                        {


                           
                            for (int i = 0; i < cityCounty.Count(); i++)
                            {

                                d++;

                                Class.CriteriaObject.TweetFilterCriteriaObject criteriaObject2 = new Class.CriteriaObject.TweetFilterCriteriaObject();
                                criteriaObject2.city = cityCounty[i].il.ToString();
                                criteriaObject2.county = cityCounty[i].ilce.ToString();
                                criteriaObject2.item = item;
                                criteriaObject2.category = category;
                                criteriaObject2.allId = tweetIdArrayJsonFormat;
                                criteriaObject2.withCoordinate = true;
                                var filteredTweet = tweetFilter.Filter(criteriaObject2);


                                if (filteredTweet != null)
                                {
                                    if (await pincidentRequestObject.POSTReport(filteredTweet) != null)
                                    {
                                        return null;
                                    }

                                    filteredTweetArray.Add(filteredTweet);
                                    

                                }

                                GC.Collect();
                            }

                            counter++;
                            GC.Collect();

                        }
                        GC.Collect();

                    }
                    GC.Collect();

                }
                GC.Collect();










            }








        /*
             strTweetId.WriteLine(tweetIdArrayJsonFormat);

                   strTweets.WriteLine(filteredTweetArray + "\r\n\r\n");
                  
                  strTweetsJsonFormat.WriteLine(tweetArrayJsonFormat);
                 

           
                   strTweetId.Close();
                   strTweetsJsonFormat.Close();
                  strTweets.Close();
                  */
                  
            GC.Collect();

            // var resp= req.get_results_delta(not_coordinates_json);




           await twitterRequestObject.GetAndLogAccountLimits();


            stopWatch.Stop();

            TimeSpan ts = stopWatch.Elapsed;


            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);


            Console.WriteLine("RunTimeInner :" + elapsedTime);
            Console.WriteLine(counter.ToString());
            Console.WriteLine(d.ToString());


            string foo = "foo";
            return foo;

        }
    }
}
