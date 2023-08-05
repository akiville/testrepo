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
	public class RovingPlanScheduledActualController : ApiController
	{
        // GET: api/RovingPlanScheduledActual
        public HttpResponseMessage Get(int id)
        {
            RovingPlanScheduledActualCollection RovingPlanScheduledActual_list = new RovingPlanScheduledActualCollection();
            RovingPlanScheduledActual roving_plan_scheduled_actual = new RovingPlanScheduledActual();

            roving_plan_scheduled_actual = RovingPlanScheduledActualManager.GetItem(id);
            RovingPlanScheduledActual_list.Add(roving_plan_scheduled_actual);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = RovingPlanScheduledActual_list.Count, rovingplanscheduledactuals = RovingPlanScheduledActual_list }, Configuration.Formatters.JsonFormatter);
        }

		// GET: api/RovingPlanScheduledActual/5
		 public HttpResponseMessage Get()
		{
		
			RovingPlanScheduledActualCollection RovingPlanScheduledActual_list = new RovingPlanScheduledActualCollection();
			RovingPlanScheduledActualCriteria RovingPlanScheduledActual_criteria = new RovingPlanScheduledActualCriteria();

			RovingPlanScheduledActual_list = RovingPlanScheduledActualManager.GetList(RovingPlanScheduledActual_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RovingPlanScheduledActual_list.Count, rovingplanscheduledactuals = RovingPlanScheduledActual_list }, Configuration.Formatters.JsonFormatter);
				
				
		}

		// POST: api/RovingPlanScheduledActual
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(RovingPlanScheduledActual RovingPlanScheduledActual)
		{
			if (RovingPlanScheduledActual.Validate())
			{
				int result;

				result = RovingPlanScheduledActualManager.Save(RovingPlanScheduledActual);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/RovingPlanScheduledActual/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RovingPlanScheduledActual/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RovingPlanScheduledActual RovingPlanScheduledActual)
		{
			try
			{
				int result;

				result = RovingPlanScheduledActualManager.Delete(RovingPlanScheduledActual);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}