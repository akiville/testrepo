using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using Qtc.Branch.Bll;
using Qtc.Branch.BusinessEntities;
using System.Web;
using System.IO;

namespace Qtc.Branch.Api.Controllers
{
    public class ProductAvailUpdateController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post()
        {
            try
            {
                var request = HttpContext.Current.Request;
                
                var branch_id = request.Form["branch_id"];
                var inventory_date = request.Form["inventory_date"];
                ProductAvailManager.UpdateInventory(Int32.Parse(branch_id), DateTime.Parse(inventory_date));
                return Request.CreateResponse(HttpStatusCode.OK, new { result = 1 }, Configuration.Formatters.JsonFormatter);
                
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
    }
}