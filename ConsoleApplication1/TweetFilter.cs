using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;


namespace ConsoleApplication1
{
    
    public class TweetFilter
    {

        
     


        /// <summary>
        ///  Takes tweet and converts it to a dynamic JObject after applying some filters.
        ///  
        /// </summary>
        /// <param name="item"> twitStatus  </param>
        /// <param name="category"></param>
        /// <param name="city"></param>
        /// <param name="county"></param>
        /// <param name="all_id"> allTweetsId</param>
        /// <returns></returns>
        public dynamic Filter(Class.CriteriaObject.TweetFilterCriteriaObject criteriaObject)
        {
            Class.Utilities utilityObject = new Class.Utilities();
            string city = criteriaObject.city;
            string category = criteriaObject.category;
            string county = criteriaObject.county;
            dynamic all_id = criteriaObject.allId;
            dynamic item = criteriaObject.item;
            if (city==null || category==null || county==null || all_id==null || item==null )
            {
                return null;
            }
           
            //TODO : variable naminig.
            GeoLocationDTO location = new GeoLocationDTO();
            dynamic jarr = new JArray();
            //TODO : resultItem
            dynamic resultItem = new JObject();


            Class.KMPAlgorithm KMP = new Class.KMPAlgorithm();


            string P1 = "" + city + "'";
            string P2 = "" + city + " ";
            string P3 = "" + county + "'";
            string P4 = "" + county + " ";

            string[] extra = new String[4] { P1, P2, P3, P4 };

            Boolean[] arr = new Boolean[4];

            Parallel.For(0, 4, tmp =>
            {
                arr[tmp] = KMP.Search(extra[tmp].ToString(), item.text.ToString());
            });

           


            if (criteriaObject.withCoordinate)
            {

                location.Latitude = item.coordinates.coordinates[0];
                location.Longitude = item.coordinates.coordinates[1];
                location.Address = null;



                resultItem.Location = location;
            }else
            {
                resultItem.Location = null;

            }


           

            resultItem.Id = null;


            if (arr[2])
            {
                

                resultItem.CreatedOn =utilityObject.TweetDate(item);

                resultItem.City = city;
                resultItem.County = county;




                
               

              
                if (item.text.ToString().Substring(0, 2).Equals("RT") != true)
                {
                   return NormalTweet(item,all_id,resultItem);
                }
                else
                {
                    if (item.retweeted_status.id == null) { return null; }
                   
                    return Retweet(item, all_id, resultItem);
                }




            }
            else if (arr[3])
            {
                resultItem.CreatedOn = utilityObject.TweetDate(item);

                resultItem.City = city;
                resultItem.County = county;







                if (item.text.ToString().Substring(0, 2).Equals("RT") != true)
                {
                    return NormalTweet(item, all_id, resultItem);
                }
                else
                {

                    return Retweet(item, all_id, resultItem);
                }
            }
            else if (arr[0])
            {
                
                resultItem.CreatedOn = utilityObject.TweetDate(item);
                if (arr[2] || arr[3] == true)
                {
                    resultItem.City = city;
                    resultItem.County = county;

                }
                else
                {
                    resultItem.City = city;
                    resultItem.County = null;

                }





                if (item.text.ToString().Substring(0, 2).Equals("RT") != true)
                {

                    return NormalTweet(item, all_id, resultItem);
                }
                else
                {
                    return Retweet(item, all_id, resultItem);
                }
            }
            else if (arr[1])
            {
                

                resultItem.CreatedOn = utilityObject.TweetDate(item);
                if (arr[2] || arr[3] == true)
                {
                    resultItem.City = city;
                    resultItem.County = county;

                }
                else
                {
                    resultItem.City = city;
                    resultItem.County = null;

                }





                if (item.text.ToString().Substring(0, 2).Equals("RT") != true)
                {

                   return NormalTweet(item, all_id, resultItem);
                }
                else
                {

                    return Retweet(item, all_id, resultItem);
                }
            }
            else
            {



                return null;
            }







        }

        private dynamic Retweet(dynamic item, dynamic all_id, dynamic resultItem) { 

             if (item.retweeted_status.id == null) { return null; }
            
            Class.Utilities utilityObject = new Class.Utilities();
            if (utilityObject.IdSearch(item.retweeted_status.id.ToString(), all_id))
                    {
                        return null;
                    }

                    resultItem.TweetScreenName = item.retweeted_status.user.screen_name.ToString();
                    resultItem.TweetId = (Int64)item.retweeted_status.id;
                    resultItem.Text = item.retweeted_status.text.ToString();


                    if (item.retweeted_status.entities.urls != null && item.retweeted_status.entities.urls.Count > 0)
                    {
                        resultItem.URL = item.retweeted_status.entities.urls[0].url.ToString();
                    }
                    else resultItem.URL = null;

                    if (item.retweeted_status.entities.media != null && item.retweeted_status.entities.media.Count > 0)
                    {
                        if (item.retweeted_status.extended_entities.media != null && item.retweeted_status.extended_entities.media[0].type.ToString() == "video")
                        {
                            resultItem.MediaURL = item.retweeted_status.extended_entities.media[0].video_info.variants[0].url;
                            resultItem.TweetURL = utilityObject.GenerateTweetURL( resultItem.TweetScreenName.ToString(), resultItem.TweetId.ToString());
                        }
                        else
                        {
                            resultItem.MediaURL = item.retweeted_status.entities.media[0].media_url;
                            resultItem.TweetURL = utilityObject.GenerateTweetURL( resultItem.TweetScreenName.ToString(), resultItem.TweetId.ToString());
                        }
                    }
                    else
                    {
                        resultItem.MediaURL = null;
                        resultItem.TweetURL = utilityObject.GenerateTweetURL( resultItem.TweetScreenName.ToString(), resultItem.TweetId.ToString());
                    }

                    all_id.Add(item.retweeted_status.id.ToString());
                    return resultItem;
                   
                }
        


        private dynamic NormalTweet(dynamic item,dynamic all_id,dynamic resultItem)
        {
            
            Class.Utilities utilityObject = new Class.Utilities();
            if (utilityObject.IdSearch(item.id.ToString(), all_id))
            {
                return null;
            }
            else
            {
                
                resultItem.TweetScreenName = item.user.screen_name.ToString();
                resultItem.TweetId = (Int64)item.id;
                resultItem.Text = item.text.ToString();

                resultItem.URL = item.entities.urls != null && item.entities.urls.Count > 0 ? item.entities.urls[0].url.ToString() : null;

              

                if (item.entities.media != null && item.entities.media.Count > 0)
                {
                    
                    if (item.extended_entities.media != null && item.extended_entities.media[0].type.ToString() == "video")
                    {
                        resultItem.MediaURL = item.extended_entities.media[0].video_info.variants[0].url;
                        
                        resultItem.TweetURL = utilityObject.GenerateTweetURL( resultItem.TweetScreenName.ToString(), resultItem.TweetId.ToString());
                    }
                    else
                    {
                        resultItem.MediaURL = item.entities.media[0].media_url;
                        resultItem.TweetURL = utilityObject.GenerateTweetURL( resultItem.TweetScreenName.ToString(), resultItem.TweetId.ToString());
                    }
                }
                else
                {
                    resultItem.MediaURL = null;
                    resultItem.TweetURL = utilityObject.GenerateTweetURL( resultItem.TweetScreenName.ToString(), resultItem.TweetId.ToString());
                }

                all_id.Add(item.id.ToString());

                return resultItem;
            }
           
        }
    






    
   


        
      


    }

    
}
