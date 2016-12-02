using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.IO;
using Fizzler.Systems.HtmlAgilityPack;

namespace uwWebRequest.Classes
{
    public class uwRequest
    {
        private Dictionary<string,string> localHeaders { get; set; }
        public const string ID_LOGIN      = @"https://weblogin.washington.edu/";
        public const string PUB_REPLY     = @"https://idp.u.washington.edu/PubCookie.reply";
        public const string EMAIL_REQUEST = @"https://advance.admin.washington.edu/advdb/action.aspx?treestate=--11-&PageId=50002&AppId=80900&idnumber=#IDNUMBER#";
        public const string TEST_REQUEST = @"https://advance.admin.washington.edu/advdb/action.aspx?treestate=--11-&PageId=50002&AppId=80900&idnumber=0001003621";
        public enum AUTH_STEP
        {
            STEP_1,
            STEP_2
        }

        public uwRequest(AUTH_STEP step)
        {
            switch (step)
            {
                case AUTH_STEP.STEP_1:
                    addStepOneAuthenticationHeaders();
                    break;
                case AUTH_STEP.STEP_2:
                    addStepTwoAuthenticationHeaders();
                    break;
                default:
                    break;
            }

        }
        
        public void addCredentials(string userId, 
            string password, 
            string token)
        { 
            addNewHeader("user",userId);
            addNewHeader("pass",password);
            addNewHeader("pass2",token);
        }

        public void addNewHeader(string Key, string Value)
        {
            this.localHeaders.Add(Key,Value);
        }

        private void addStepOneAuthenticationHeaders()
        {
            this.localHeaders = new Dictionary<string, string>(){
                {"one"                       ,"idp.u.washington.edu"},
                {"two"                       ,"uwtoken30"},
                {"creds_from_greq"           ,"3"},
                {"three"                     ,"3"},
                {"four"                      ,"a5"},
                {"five"                      ,"GET"},
                {"six"                       ,"idp.u.washington.edu"},
                {"seven"                     ,"L2lkcC9BdXRobi9VV0xvZ2luVG9rZW4zMA=="},
                {"relay_url"                 ,"https://idp.u.washington.edu/PubCookie.reply"},
                {"eight"                     ,"Y29udmVyc2F0aW9uPWUyczE6NA=="},
                {"fr"                        ,"NFR"},
                {"hostname"                  ,"idp.u.washington.edu"},
                {"nine"                      ,"1"},
                {"file"                      ,"" },
                {"flag"                      ,"0"},
                {"referer"                   ,"(null)"},
                {"post_stuff"                ,""},
                {"sess_re"                   ,"0"},
                {"pre_sess_tok"              ,"1494352044"},
                {"first_kiss"                ,"1480541706-166024"},
                {"pinit"                     ,"0"},
                {"reply"                     ,"1"},
                {"upg"                       ,"0"}
            };
        }
        private void addStepTwoAuthenticationHeaders()
        {
            this.localHeaders = new Dictionary<string, string>(){
                { "Host",                       "advance.admin.washington.edu"},
                { "Connection",                 "keep-alive"},
                { "Cache-Control",              "max-age=0"},
                { "Upgrade-Insecure-Requests",  ""},
                { "User-Agent",                 "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36"},
                { "Accept",                     "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8"},
                { "DNT",                        "1"},
                { "Referer",                    "https://idp.u.washington.edu/idp/profile/SAML2/Redirect/SSO?execution=e1s1&_eventId_proceed=1"},
                { "Accept-Encoding",            "gzip, deflate, sdch, br"},
                { "Accept-Language",            "en-US,en;q=0.8"},
                { "Cookie",                     "ASP.NET_SessionIdADVDB=ew3r414gf0hayigssdtys4qb; AuthToken=aa254600-ceb4-44ac-8eb8-83b767b58d53; __utma=80390417.2037367272.1480541522.1480541522.1480541522.1; __utmc=80390417; __utmz=80390417.1480541522.1.1.utmcsr=myuw.washington.edu|utmccn=(referral)|utmcmd=referral|utmcct=/servlet/user; _shibsession_64656661756c7468747470733a2f2f616476616e63652e61646d696e2e77617368696e67746f6e2e6564752f73686962626f6c657468=_5bebf0520d8e3804e7ee2584cb8a602a"},
            };        
        }

        public static string serializeHeaders(Dictionary<string, string> headers)
        {
            StringBuilder postData = new StringBuilder();

            foreach (var key in headers.Keys)
            {
                string tempData = String.Concat("&", key, "=", headers[key]);

                postData.Append(tempData);
            }

            return postData.ToString();
        }

        public Dictionary<string,string> getHeaders()
        {
            return this.localHeaders;
        }

        public static string Post(string endpointURL, Dictionary<string, string> headers)
        {
            var request = (HttpWebRequest)WebRequest.Create(endpointURL);

            //var postData = serializeHeaders(headers);

            //var data = Encoding.ASCII.GetBytes(postData);
            
            foreach (var key in headers.Keys)
            {                
                string val = headers[key].ToString();


                if (headers[key] == "Cookie") {
                    request.Headers.Add(key, val);
                }
                
                             
            }

         

            request.Method = "GET";
            //request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = data.Length;

            /*
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            */

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return responseString;
        }

        public static string Get(string endpointURL)
        {
            var request = (HttpWebRequest)WebRequest.Create(endpointURL);

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return responseString;
        }

        public static Dictionary<string, string> loadHtmlGetElementsBySelector(string html, string selector)
        {
            var source = new HtmlAgilityPack.HtmlDocument();

            source.LoadHtml(html);

            return getNameValueByElementType(source, selector);
        }

        private static Dictionary<string, string> getNameValueByElementType(
            HtmlAgilityPack.HtmlDocument source, 
            string selector) {

            Dictionary<string, string> output = new Dictionary<string, string>();
            var document = source.DocumentNode;

            IEnumerable<HtmlAgilityPack.HtmlNode> nodes = document.QuerySelectorAll(selector);

            foreach (HtmlAgilityPack.HtmlNode node in nodes)
            {
                if (node.Attributes["type"].Value == "hidden") { 
                    output.Add(node.Attributes["name"].Value,node.Attributes["value"].Value);
                }
            }

            return output;
        }
    }
}
