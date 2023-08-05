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
	public class RovingPlanScheduledActualChecklistController : ApiController
	{
        // GET: api/RovingPlanScheduledActualChecklist
        public HttpResponseMessage Get(int id)
        {
            RovingPlanScheduledActualChecklistCollection RovingPlanScheduledActualChecklist_list = new RovingPlanScheduledActualChecklistCollection();
            RovingPlanScheduledActualChecklist roving_plan_scheduled_actual_checklist = new RovingPlanScheduledActualChecklist();
            roving_plan_scheduled_actual_checklist = RovingPlanScheduledActualChecklistManager.GetItem(id);
            RovingPlanScheduledActualChecklist_list.Add(roving_plan_scheduled_actual_checklist);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = RovingPlanScheduledActualChecklist_list.Count, rovingplanscheduledactualchecklists = RovingPlanScheduledActualChecklist_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/RovingPlanScheduledActualChecklist/5
        public HttpResponseMessage Get(int rps_id, int employee_id)
		{
			
			RovingPlanScheduledActualChecklistCollection RovingPlanScheduledActualChecklist_list = new RovingPlanScheduledActualChecklistCollection();
			RovingPlanScheduledActualChecklistCriteria RovingPlanScheduledActualChecklist_criteria = new RovingPlanScheduledActualChecklistCriteria();

            RovingPlanScheduledActualChecklist_criteria.mRpsId = rps_id;
            RovingPlanScheduledActualChecklist_list = RovingPlanScheduledActualChecklistManager.GetList(RovingPlanScheduledActualChecklist_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RovingPlanScheduledActualChecklist_list.Count, rovingplanscheduledactualchecklists = RovingPlanScheduledActualChecklist_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/RovingPlanScheduledActualChecklist
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(RovingPlanScheduledActualChecklist RovingPlanScheduledActualChecklist)
		{
			if (RovingPlanScheduledActualChecklist.Validate())
			{
				int result;

				result = RovingPlanScheduledActualChecklistManager.Save(RovingPlanScheduledActualChecklist);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/RovingPlanScheduledActualChecklist/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RovingPlanScheduledActualChecklist/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RovingPlanScheduledActualChecklist RovingPlanScheduledActualChecklist)
		{
			try
			{
				int result;

				result = RovingPlanScheduledActualChecklistManager.Delete(RovingPlanScheduledActualChecklist);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}