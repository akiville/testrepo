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
	public class SalesSchedulingColorPlannedController : ApiController
	{
		// GET: api/SalesSchedulingColorPlanned
		 public HttpResponseMessage Get(int id)
		{
			SalesSchedulingColorPlannedCollection SalesSchedulingColorPlanned_list = new SalesSchedulingColorPlannedCollection();
			SalesSchedulingColorPlanned SalesSchedulingColorPlanned = new SalesSchedulingColorPlanned();
			SalesSchedulingColorPlanned = SalesSchedulingColorPlannedManager.GetItem(id);
			SalesSchedulingColorPlanned_list.Add(SalesSchedulingColorPlanned);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  SalesSchedulingColorPlanned_list.Count, salesschedulingcolorplanneds = SalesSchedulingColorPlanned_list }, Configuration.Formatters.JsonFormatter);
		}

		// GET: api/SalesSchedulingColorPlanned/5
		 public HttpResponseMessage Get()
		{
			SalesSchedulingColorPlannedCollection SalesSchedulingColorPlanned_list = new SalesSchedulingColorPlannedCollection();
			SalesSchedulingColorPlannedCriteria SalesSchedulingColorPlanned_criteria = new SalesSchedulingColorPlannedCriteria();

			SalesSchedulingColorPlanned_list = SalesSchedulingColorPlannedManager.GetList(SalesSchedulingColorPlanned_criteria);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  SalesSchedulingColorPlanned_list.Count, salesschedulingcolorplanneds = SalesSchedulingColorPlanned_list }, Configuration.Formatters.JsonFormatter);
		}

		// POST: api/SalesSchedulingColorPlanned
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(SalesSchedulingColorPlanned SalesSchedulingColorPlanned)
		{
			if (SalesSchedulingColorPlanned.Validate())
			{
				int result;

				result = SalesSchedulingColorPlannedManager.Save(SalesSchedulingColorPlanned);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/SalesSchedulingColorPlanned/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/SalesSchedulingColorPlanned/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(SalesSchedulingColorPlanned SalesSchedulingColorPlanned)
		{
			try
			{
				int result;

				result = SalesSchedulingColorPlannedManager.Delete(SalesSchedulingColorPlanned);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}