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
	public class SalesSchedulingUpdatesController : ApiController
	{
		// GET: api/SalesSchedulingUpdates
		public HttpResponseMessage Get(Int32 id)
		{
            try
            {
                SalesSchedulingUpdates salesSchedulingUpdate = new SalesSchedulingUpdates();

                salesSchedulingUpdate = SalesSchedulingUpdatesManager.GetItem(id);

                return Request.CreateResponse(HttpStatusCode.OK, new { count = 1, salesScheculingUpdate = salesSchedulingUpdate }, Configuration.Formatters.JsonFormatter);

            }
			catch
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			}
}

		// GET: api/SalesSchedulingUpdates/5
		 public HttpResponseMessage Get(int lmm_id, DateTime date, int sales_scheduling_id)
		{
			//try
			//{
				SalesSchedulingUpdatesCollection SalesSchedulingUpdates_list = new SalesSchedulingUpdatesCollection();
				SalesSchedulingUpdatesCriteria SalesSchedulingUpdates_criteria = new SalesSchedulingUpdatesCriteria();

                SalesSchedulingUpdates_criteria.mDate = date;
                SalesSchedulingUpdates_criteria.mLmmId = lmm_id;
            SalesSchedulingUpdates_criteria.mSalesSchedulingId = sales_scheduling_id;
            SalesSchedulingUpdates_list = SalesSchedulingUpdatesManager.GetList(SalesSchedulingUpdates_criteria);
				//if (SalesSchedulingUpdates_list.Count > 0 ) 
				//{
					return Request.CreateResponse(HttpStatusCode.OK, new { count =  SalesSchedulingUpdates_list.Count, salesschedulingupdatess = SalesSchedulingUpdates_list }, Configuration.Formatters.JsonFormatter);
				//}
				//else
				//{
				//	return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
				//}
			//}
			//catch
			//{
			//	return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			//}
		}

		// POST: api/SalesSchedulingUpdates
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(SalesSchedulingUpdates SalesSchedulingUpdates)
		{
			if (SalesSchedulingUpdates.Validate())
			{
				int result;

				result = SalesSchedulingUpdatesManager.Save(SalesSchedulingUpdates);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/SalesSchedulingUpdates/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/SalesSchedulingUpdates/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(int id)
		{
			//try
			//{
				int result;

				result = SalesSchedulingUpdatesManager.Delete(id);
                return Request.CreateResponse(HttpStatusCode.OK, new { id = result }, Configuration.Formatters.JsonFormatter);

                //return Request.CreateResponse(HttpStatusCode.OK, result);
   //         }
			//catch
			//{
			// return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			//}
		}
	}
}