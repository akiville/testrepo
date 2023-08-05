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
	public class RovingCheckListViolationController : ApiController
	{
		// GET: api/RovingCheckListViolation
		 public HttpResponseMessage Get(int id)
		{
			RovingCheckListViolationCollection RovingCheckListViolation_list = new RovingCheckListViolationCollection();
			RovingCheckListViolation RovingCheckListViolation = new RovingCheckListViolation();
			RovingCheckListViolation = RovingCheckListViolationManager.GetItem(id);
			RovingCheckListViolation_list.Add(RovingCheckListViolation);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RovingCheckListViolation_list.Count, rovingchecklistviolations = RovingCheckListViolation_list }, Configuration.Formatters.JsonFormatter);
		}

		// GET: api/RovingCheckListViolation/5
		 public HttpResponseMessage Get(int roving_checklist_id, String dummy)
		{
			RovingCheckListViolationCollection RovingCheckListViolation_list = new RovingCheckListViolationCollection();
			RovingCheckListViolationCriteria RovingCheckListViolation_criteria = new RovingCheckListViolationCriteria();

            RovingCheckListViolation_criteria.mRovingChecklistId = roving_checklist_id;

            RovingCheckListViolation_list = RovingCheckListViolationManager.GetList(RovingCheckListViolation_criteria);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RovingCheckListViolation_list.Count, rovingchecklistviolations = RovingCheckListViolation_list }, Configuration.Formatters.JsonFormatter);
		}

		// POST: api/RovingCheckListViolation
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(RovingCheckListViolation RovingCheckListViolation)
		{
			if (RovingCheckListViolation.Validate())
			{
				int result;

				result = RovingCheckListViolationManager.Save(RovingCheckListViolation);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/RovingCheckListViolation/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RovingCheckListViolation/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RovingCheckListViolation RovingCheckListViolation)
		{
			try
			{
				int result;

				result = RovingCheckListViolationManager.Delete(RovingCheckListViolation);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}