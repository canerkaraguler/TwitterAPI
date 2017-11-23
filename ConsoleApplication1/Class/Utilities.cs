using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Class
{
    public class Utilities
    {
        /// <summary>
        /// Searchs that if the current tweet is visited before . if it is visited returns true else returns false 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="all_id"></param>
        /// <returns></returns>
        public Boolean IdSearch(string id, dynamic all_id)
        {

            if (id == null || all_id == null)
            {
                return false;
            }
            Boolean x = false;
            foreach (var ids in all_id)
            {
                if (id == ids.ToString())
                {
                    x = true;
                    return x;
                }
            }
            return x;

        }

       /// <summary>
       /// Generates tweet url with using screen name and tweet id
       /// </summary>
       /// <param name="screenName"></param>
       /// <param name="tweetId"></param>
       /// <returns></returns>
       public String GenerateTweetURL(string screenName,string tweetId)
        {
            string URL=string.Format("http://twitter.com/{0}/status/{1}", screenName, tweetId);
            return URL;
        }

       /// <summary>
       /// Gets the created_time title from dynamic object and converts ıt to a datetime object.
       /// </summary>
       /// <param name="item"></param>
       /// <returns></returns>
       public DateTime TweetDate(dynamic item)
        {
            return DateTime.ParseExact(item.created_at.ToString(), "ddd MMM dd HH:mm:ss +0000 yyyy",
                                        System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
