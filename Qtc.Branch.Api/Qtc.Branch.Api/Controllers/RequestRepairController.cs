using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using Qtc.Branch.Bll;
using Qtc.Branch.BusinessEntities;


namespace Qtc.Branch.Api.Controllers
{
	public class RequestRepairController : ApiController
	{
        //// GET: api/RequestRepair
        public HttpResponseMessage Get(int id)
        {
            RequestRepairCollection RepairRequest_list = new RequestRepairCollection();
            RequestRepair RequestRepair = new RequestRepair();
            RequestRepair = RequestRepairManager.GetItem(id);
            RepairRequest_list.Add(RequestRepair);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = RepairRequest_list.Count, requestrepairs = RepairRequest_list }, Configuration.Formatters.JsonFormatter);
        }

        // GET: api/RequestRepair/5
        public HttpResponseMessage Get(int branch_id, int requested_by_id, int product_id, DateTime start_date, DateTime end_date)
		{
			RequestRepairCollection RequestRepair_list = new RequestRepairCollection();
			RequestRepairCriteria RequestRepair_criteria = new RequestRepairCriteria();
            RequestRepair_criteria.mBranchId = branch_id;
            RequestRepair_criteria.mRequestedById = requested_by_id;
            RequestRepair_criteria.mProductId = product_id;
            RequestRepair_criteria.mStartDate = start_date;
            RequestRepair_criteria.mEndDate = end_date;

            RequestRepair_list = RequestRepairManager.GetList(RequestRepair_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RequestRepair_list.Count, requestrepairs = RequestRepair_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/RequestRepair
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(RequestRepair RequestRepair)
		{
			if (RequestRepair.Validate())
			{
				int result;

				result = RequestRepairManager.Save(RequestRepair);

                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/RequestRepair/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RequestRepair/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RequestRepair RequestRepair)
		{
			try
			{
				int result;

				result = RequestRepairManager.Delete(RequestRepair);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}