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
	public class RovingPlanController : ApiController
	{
		// GET: api/RovingPlan
		public HttpResponseMessage Get(int id)
        {
            RovingPlanCollection RovingPlan_list = new RovingPlanCollection();
            RovingPlan roving_plan = new RovingPlan();
            roving_plan = RovingPlanManager.GetItem(id);
            RovingPlan_list.Add(roving_plan);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = RovingPlan_list.Count, rovingplans = RovingPlan_list }, Configuration.Formatters.JsonFormatter);
        }

		// GET: api/RovingPlan/5
		 public HttpResponseMessage Get(DateTime start_date, DateTime end_date)
		{
		
			RovingPlanCollection RovingPlan_list = new RovingPlanCollection();
			RovingPlanCriteria RovingPlan_criteria = new RovingPlanCriteria();

            RovingPlan_criteria.mStartDate = start_date;
            RovingPlan_criteria.mEndDate = end_date;

            RovingPlan_list = RovingPlanManager.GetList(RovingPlan_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RovingPlan_list.Count, rovingplans = RovingPlan_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/RovingPlan
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(RovingPlan RovingPlan)
		{
			if (RovingPlan.Validate())
			{
				int result;

				result = RovingPlanManager.Save(RovingPlan);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/RovingPlan/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RovingPlan/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RovingPlan RovingPlan)
		{
			try
			{
				int result;

				result = RovingPlanManager.Delete(RovingPlan);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}