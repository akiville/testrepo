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
	public class RovingPlanScheduledActualChecklistLSVController : ApiController
	{
		// GET: api/RovingPlanScheduledActualChecklistLSV
		 public HttpResponseMessage Get(int id)
		{
			RovingPlanScheduledActualChecklistLSVCollection RovingPlanScheduledActualChecklistLSV_list = new RovingPlanScheduledActualChecklistLSVCollection();
			RovingPlanScheduledActualChecklistLSV RovingPlanScheduledActualChecklistLSV = new RovingPlanScheduledActualChecklistLSV();
			RovingPlanScheduledActualChecklistLSV = RovingPlanScheduledActualChecklistLSVManager.GetItem(id);
			RovingPlanScheduledActualChecklistLSV_list.Add(RovingPlanScheduledActualChecklistLSV);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RovingPlanScheduledActualChecklistLSV_list.Count, rovingplanscheduledactualchecklistlsvs = RovingPlanScheduledActualChecklistLSV_list }, Configuration.Formatters.JsonFormatter);
		}

		// GET: api/RovingPlanScheduledActualChecklistLSV/5
		 public HttpResponseMessage Get(int rps_id, int rps_chklist_id)
		{
			RovingPlanScheduledActualChecklistLSVCollection RovingPlanScheduledActualChecklistLSV_list = new RovingPlanScheduledActualChecklistLSVCollection();
			RovingPlanScheduledActualChecklistLSVCriteria RovingPlanScheduledActualChecklistLSV_criteria = new RovingPlanScheduledActualChecklistLSVCriteria();

            RovingPlanScheduledActualChecklistLSV_criteria.mRpsId = rps_id;
            RovingPlanScheduledActualChecklistLSV_criteria.mRpsChklistId = rps_chklist_id;
            RovingPlanScheduledActualChecklistLSV_list = RovingPlanScheduledActualChecklistLSVManager.GetList(RovingPlanScheduledActualChecklistLSV_criteria);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RovingPlanScheduledActualChecklistLSV_list.Count, rovingplanscheduledactualchecklistlsvs = RovingPlanScheduledActualChecklistLSV_list }, Configuration.Formatters.JsonFormatter);
		}

		// POST: api/RovingPlanScheduledActualChecklistLSV
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(RovingPlanScheduledActualChecklistLSV RovingPlanScheduledActualChecklistLSV)
		{
			if (RovingPlanScheduledActualChecklistLSV.Validate())
			{
				int result;

				result = RovingPlanScheduledActualChecklistLSVManager.Save(RovingPlanScheduledActualChecklistLSV);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/RovingPlanScheduledActualChecklistLSV/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RovingPlanScheduledActualChecklistLSV/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RovingPlanScheduledActualChecklistLSV RovingPlanScheduledActualChecklistLSV)
		{
			try
			{
				int result;

				result = RovingPlanScheduledActualChecklistLSVManager.Delete(RovingPlanScheduledActualChecklistLSV);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}