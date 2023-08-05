using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Qtc.Branch.BusinessEntities;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Configuration;
using Qtc.qPos.Web;
using Newtonsoft.Json;

namespace Qtc.Branch.Web.Controllers
{
    public static class DropDownHelper
    {
        public static BranchsCollection getBranch(int lmm_id)
        {
            BranchsCollection branchCollection = new BranchsCollection();
            using (var client = new HttpClient())
            {
                HttpClientManager.ClientHeaders(client);

                //HTTP POST
                var postTask = client.GetStringAsync(ConfigurationManager.AppSettings["APIURL"] + "/Branchs?name=ALL&lmm_id=" + lmm_id);

                postTask.Wait();

                var result = postTask.Result;

                if (result != null & result.Length > 50)
                {
                    var parsedObject = JObject.Parse(result);
                    //var userObject = (JObject)parsedObject["users"].ToObject<List<User>>();

                    branchCollection = JsonConvert.DeserializeObject<BranchsCollection>(parsedObject["branches"].ToString());

                }
                //else
                //{
                //    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                //}

                return branchCollection;
            }
        }

    }
}