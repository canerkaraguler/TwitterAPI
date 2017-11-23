using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Class.CriteriaObject
{
    public class TweetFilterCriteriaObject
    {
        public dynamic item { get; set; }
        public string category { get; set; }
        public string city { get; set; }
        public string county { get; set; }
        public dynamic allId { get; set; }
        public bool withCoordinate { get; set; }
    }
}
