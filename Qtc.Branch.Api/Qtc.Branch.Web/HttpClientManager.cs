using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace Qtc.qPos.Web
{
    public class HttpClientManager
    {
        public static void ClientHeaders(HttpClient client)
        {
            string api_url = ConfigurationManager.AppSettings["APIURL"];
            client.DefaultRequestHeaders.Clear();
            client.BaseAddress = new Uri(api_url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",
                HttpContext.Current.Session["TokenNumber"] + ":" + HttpContext.Current.Session["Username"]);
        }
    }
}