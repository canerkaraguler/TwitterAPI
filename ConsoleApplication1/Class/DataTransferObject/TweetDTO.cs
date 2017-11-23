using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class TweetDTO
    {
        /// <summary>
        /// Identifier for our system.
        /// </summary>
        public int? Id { get; set; }//

        public DateTime? CreatedOn { get; set; }//

        public string City { get; set; }

        public string County { get; set; }

        /// <summary>
        /// Tweet Identifier for Twitter API.
        /// </summary>
        public Int64 TweetId { get; set; }

        /// <summary>
        /// Who send this tweet.
        /// </summary>
        public string TweetScreenName { get; set; }

        /// <summary>
        /// Content of tweet.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// if text includes any URL.
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// If tweet has any media attachment, its source url.
        /// </summary>
        public string MediaURL { get; set; }

        /// <summary>
        /// Tweet's orginal tweeter.com source link.
        /// </summary>
        public string TweetURL { get; set; }

        /// <summary>
        /// Location of tweet
        /// </summary>
        public GeoLocationDTO Location { get; set; }

    }
}
