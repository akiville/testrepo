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
	public class RovingPlanScheduledController : ApiController
	{
		// GET: api/RovingPlanScheduled
		public HttpResponseMessage Get(int id)
        {
            RovingPlanScheduledCollection RovingPlanScheduled_list = new RovingPlanScheduledCollection();
            RovingPlanScheduled roving_plan_schedule = new RovingPlanScheduled();

            roving_plan_schedule = RovingPlanScheduledManager.GetItem(id);
            RovingPlanScheduled_list.Add(roving_plan_schedule);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = RovingPlanScheduled_list.Count, rovingplanscheduleds = RovingPlanScheduled_list }, Configuration.Formatters.JsonFormatter);
        }

		// GET: api/RovingPlanScheduled/5
		 public HttpResponseMessage Get(int roving_plan_id, int roving_plan_oic_id, DateTime start_date, DateTime end_date)
		{
			
			RovingPlanScheduledCollection RovingPlanScheduled_list = new RovingPlanScheduledCollection();
			RovingPlanScheduledCriteria RovingPlanScheduled_criteria = new RovingPlanScheduledCriteria();

            RovingPlanScheduled_criteria.mRovingPlanId = roving_plan_id;
            RovingPlanScheduled_criteria.mRovingPlanOicId = roving_plan_oic_id;
            RovingPlanScheduled_criteria.mStartDate = start_date;
            RovingPlanScheduled_criteria.mEndDate = end_date;


            RovingPlanScheduled_list = RovingPlanScheduledManager.GetList(RovingPlanScheduled_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RovingPlanScheduled_list.Count, rovingplanscheduleds = RovingPlanScheduled_list }, Configuration.Formatters.JsonFormatter);
			
		}

		// POST: api/RovingPlanScheduled
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(RovingPlanScheduled RovingPlanScheduled)
		{
			if (RovingPlanScheduled.Validate())
			{
				int result;

				result = RovingPlanScheduledManager.Save(RovingPlanScheduled);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/RovingPlanScheduled/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RovingPlanScheduled/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RovingPlanScheduled RovingPlanScheduled)
		{
			try
			{
				int result;

				result = RovingPlanScheduledManager.Delete(RovingPlanScheduled);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}