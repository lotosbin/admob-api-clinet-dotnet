using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using Binbin.HttpHelper;

namespace Binbin.AdMobApi
{
    public class AdMobApiReturn<TData>
        where TData : class
    {
        public AdMobApiReturn()
        {
            this.errors = new List<string>();
            this.warnings = new List<string>();
            this.data = new List<TData>();
            this.page = new AdMobApiPage();

        }
        public List<string> errors { get; set; }
        public List<string> warnings { get; set; }
        public List<TData> data { get; set; }
        public AdMobApiPage page { get; set; }
    }

    public class AdMobApiPage
    {
        public int current { get; set; }
        public int total { get; set; }
    }

    public class AdMobApiClientV2
    {
        private string client_key;
        private string email;
        private string password;
        public string token;
        public AdMobApiClientV2(string clientKey, string email, string password)
        {
            this.client_key = clientKey;
            this.email = email;
            this.password = password;
        }

        public string AuthLogin()
        {
            var paras = new List<APIParameter>()
                            {
                                new APIParameter("client_key", this.client_key),
                                new APIParameter("email",(this.email)),
                                new APIParameter("password", (this.password)),
                            };
            string result = new SyncHttpRequest().HttpPost("https://api.admob.com/v2/auth/login", paras);
            if (result.Contains("token"))
            {
                var startTag = "\"token\":\"";
                int startIndex = result.IndexOf(startTag) + startTag.Length;
                int endIndex = result.IndexOf("\"", startIndex);
                this.token = result.Substring(startIndex, endIndex - startIndex);
            }
            return result;
        }
        public string AuthLogout()
        {
            var paras = new List<APIParameter>()
                            {
                                new APIParameter("client_key", this.client_key),
                                new APIParameter("token",(this.token)),
                            };
            string result = new SyncHttpRequest().HttpPost("https://api.admob.com/v2/auth/logout", paras);
            return result;
        }
        public string SiteSearch()
        {
            var paras = new List<APIParameter>()
                            {
                                new APIParameter("client_key", this.client_key),
                                new APIParameter("token",(this.token)),
                            };
            string result = new SyncHttpRequest().HttpGet("https://api.admob.com/v2/site/search", paras);
            return result;
        }
        public AdMobApiReturn<AdMobApiSiteSearchData> SiteSearch2()
        {
            var result = this.SiteSearch();
            var serializer= new JavaScriptSerializer();
            var r = serializer.Deserialize<AdMobApiReturn<AdMobApiSiteSearchData>>(result);
            return r;
        }
    }
    public class AdMobApiSiteSearchData
    {
        public string id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string description { get; set; }
    }
}