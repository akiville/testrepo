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
	public class SalesSchedulingConcernController : ApiController
	{
		// GET: api/SalesSchedulingConcern
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET: api/SalesSchedulingConcern/5
		 public HttpResponseMessage Get(int sales_scheduling_id, int branch_id, int lmm_id, String status)
		{
			
			SalesSchedulingConcernCollection SalesSchedulingConcern_list = new SalesSchedulingConcernCollection();
			SalesSchedulingConcernCriteria SalesSchedulingConcern_criteria = new SalesSchedulingConcernCriteria();

            SalesSchedulingConcern_criteria.mSalesSchedulingId = sales_scheduling_id;
            SalesSchedulingConcern_criteria.mBranchId = branch_id;
            SalesSchedulingConcern_criteria.mLmmId = lmm_id;
            SalesSchedulingConcern_criteria.mStatus = status;

            SalesSchedulingConcern_list = SalesSchedulingConcernManager.GetList(SalesSchedulingConcern_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  SalesSchedulingConcern_list.Count, salesschedulingconcerns = SalesSchedulingConcern_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/SalesSchedulingConcern
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(SalesSchedulingConcern SalesSchedulingConcern)
		{
			if (SalesSchedulingConcern.Validate())
			{
				int result;

				result = SalesSchedulingConcernManager.Save(SalesSchedulingConcern);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/SalesSchedulingConcern/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/SalesSchedulingConcern/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(SalesSchedulingConcern SalesSchedulingConcern)
		{
			try
			{
				int result;

				result = SalesSchedulingConcernManager.Delete(SalesSchedulingConcern);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}