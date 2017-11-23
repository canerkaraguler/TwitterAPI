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


namespace ConsoleApplication1.Class
{
    public abstract class Request
    {
        protected string baseURL { get; set; }
        protected string appendLimitURL { get; set; }
        protected string appendSearchURL { get; set; }

        protected abstract bool CheckConnection();

        protected abstract string GetToken();


        

    }
}
