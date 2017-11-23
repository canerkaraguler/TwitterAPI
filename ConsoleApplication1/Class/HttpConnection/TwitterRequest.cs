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
    public class TwitterRequest : Request
    {

        public TwitterRequest() {
           base.baseURL= "https://api.twitter.com/1.1/";
            base.appendLimitURL = "application/rate_limit_status.json?resources=help,users,search,statuses";
            base.appendSearchURL = "search/tweets.json?q={1}&result_type=recent&count=100&lang=tr&from={0}";


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


        protected override string GetToken()
        {

            

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


      

        //TODO : GetAndLogAccountLimits -> TwitterReqeust
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bearerToken"></param>
        public async Task<bool> GetAndLogAccountLimits()
        {

            if (CheckConnection() != true)
            {
                Console.WriteLine("No Connection!!!!!");
                return false;
                
            }
           
            
                var bearerToken = GetToken();
                if (bearerToken == null) { return false; }
                try
                {
                    var request2 = new HttpRequestMessage(HttpMethod.Get, string.Format(baseURL + appendLimitURL));

                    request2.Headers.Add("Authorization", "Bearer " + GetToken());//benım kendi apı hesabımın tokenı şirket adına apı hesabı olusturulunca değişcek

                    var httpClient2 = new HttpClient();

                    HttpResponseMessage response2 = await httpClient2.SendAsync(request2).ConfigureAwait(false);

                    var d = await response2.Content.ReadAsStringAsync();

                    // File.WriteAllText(@HyperConfig.Limit, d);// çok gerekli değil sadece string formatında yazıyor
                    httpClient2.Dispose();
                return true;
                }
                catch (Exception e)
                {
                Console.WriteLine(e.ToString());
                return false;
                    
                }

            
        }

       

        //TODO : GetTweets -> TwitterRequest
        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <param name="bearerToken"></param>
        /// <param name="screenName"></param>
        /// <returns>
        /// 
        /// </returns>
        public async Task<String> GetTweets(string category,  string screenName)
        {

            if (CheckConnection() != true)
            {
                Console.WriteLine("not connected");
                return null;
            }
            var bearerToken = GetToken();
            if (bearerToken==null) { return null; }
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, string.Format(baseURL+appendSearchURL, screenName, category));//burada sadece screen name olarak yarlasan yeter


            request.Headers.Add("Authorization", "Bearer " + bearerToken);

            var httpClient = new HttpClient();
           
                HttpResponseMessage response = await httpClient.SendAsync(request).ConfigureAwait(false);

                var c = await response.Content.ReadAsStringAsync();
                httpClient.Dispose();
                if (String.IsNullOrEmpty(c))
                {
                    return null;
                }
                return c;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }



            //TDODO: IDisposable objeler abstract class'ta tutuldupunda ne yapmalıyız ? tutulmalı mı ?++++++++++

          
        }
    }
}
