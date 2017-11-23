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
    public class PincidentRequest : Request
    {
        public PincidentRequest()
        {
            base.baseURL = "";

        }

        protected override bool CheckConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    
                    return true;
                }
            }
            catch
            {   

                return false;
            }


        }

       

        


        //TODO : POSTReport() -> PincidenRequest
        //TODO : Future work : POSTReport(TweetDTO tweet)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public async Task<string> POSTReport(JObject json)
        {
            if(CheckConnection() != true)
            {
                Console.WriteLine("not connected");
                return null;
            }

            

            try
            {

                var httpClient = new HttpClient();
                string url =HyperConfig.postReportBaseUrl+"api/validateTweet";

                httpClient.DefaultRequestHeaders.Add("Authorization", ConfigurationManager.AppSettings["Bearer"]);
                string postData = JsonConvert.SerializeObject(json);

                var content = new StringContent(postData, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, content);

                var result = await response.Content.ReadAsStringAsync();
                httpClient.Dispose();


                if (String.IsNullOrEmpty(result))
                {
                    return null;
                }

                 dynamic req = JsonConvert.DeserializeObject<dynamic>(result);





                 switch ((int)req.Status)
                 {
                     case 1:
                         StreamWriter log_status1 = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(),HyperConfig.logStatus1), append: true);
                         log_status1.WriteLine(req + "\r\n" + json);


                         log_status1.Close();
                         break;

                     case 2:
                         StreamWriter log_status2 = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), HyperConfig.logStatus2), append: true);
                         log_status2.WriteLine(req + "\r\n" + json);

                         log_status2.Close();
                         break;
                     case 3:
                         StreamWriter log_status3 = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), HyperConfig.logStatus3), append: true);
                         log_status3.WriteLine(req + "\r\n" + json);

                         log_status3.Close();
                         break;
                     default:
                         Console.Write("log hatası !!!!!");
                         break;

                 }


                return result;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            


        }

        protected override string GetToken()
        {

            //token request gibi bir varaible ismi verelim.+++++++++

            try
            {
                WebRequest tokenRequest = WebRequest.Create("https://api.twitter.com/oauth2/token");


                string consumerKeyAndSecret = String.Format("{0}:{1}", HyperConfig.consumerKey, HyperConfig.consumerSecret);

                tokenRequest.Method = "POST";
                tokenRequest.Headers.Add("Authorization", String.Format("Basic {0}", Convert.ToBase64String(Encoding.UTF8.GetBytes(consumerKeyAndSecret))));

                tokenRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";

                string postData = "grant_type=client_credentials";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                tokenRequest.ContentLength = byteArray.Length;
                Stream dataStream = tokenRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();


                //TOKEN ALMAK istediğinde exception durumlarının handle edilmesi
                // 1. bağlantu yok ?++++++++++++++
                // 2. token vermeye bilir.++++++++++++++++

                WebResponse tokenResponse = tokenRequest.GetResponse();

                Stream stream = tokenResponse.GetResponseStream();
                StreamReader sr = new StreamReader(stream);

                string str = sr.ReadLine();

                dynamic jsontoken = JsonConvert.DeserializeObject<dynamic>(str);

                if (String.IsNullOrEmpty(jsontoken.access_token.ToString()))
                {
                    return null;
                }
                return jsontoken.access_token.ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                return null;
            }
        }
    }
}
