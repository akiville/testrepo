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
	public class RovingCheckListViolationPersonnelController : ApiController
	{
		// GET: api/RovingCheckListViolationPersonnel
		 public HttpResponseMessage Get(int id)
		{
			RovingCheckListViolationPersonnelCollection RovingCheckListViolationPersonnel_list = new RovingCheckListViolationPersonnelCollection();
			RovingCheckListViolationPersonnel RovingCheckListViolationPersonnel = new RovingCheckListViolationPersonnel();
			RovingCheckListViolationPersonnel = RovingCheckListViolationPersonnelManager.GetItem(id);
			RovingCheckListViolationPersonnel_list.Add(RovingCheckListViolationPersonnel);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RovingCheckListViolationPersonnel_list.Count, rovingchecklistviolationpersonnels = RovingCheckListViolationPersonnel_list }, Configuration.Formatters.JsonFormatter);
		}

		// GET: api/RovingCheckListViolationPersonnel/5
		 public HttpResponseMessage Get(int rps_id, int rps_chklist_id, int violation_id, int rclvd_detail_id, int roving_checklist_violation_id, int employee_id)
		{
			RovingCheckListViolationPersonnelCollection RovingCheckListViolationPersonnel_list = new RovingCheckListViolationPersonnelCollection();
			RovingCheckListViolationPersonnelCriteria RovingCheckListViolationPersonnel_criteria = new RovingCheckListViolationPersonnelCriteria();
            RovingCheckListViolationPersonnel_criteria.mRpsId = rps_id;
            RovingCheckListViolationPersonnel_criteria.mRpsChklistId = rps_chklist_id;
            RovingCheckListViolationPersonnel_criteria.mViolationId = violation_id;
            RovingCheckListViolationPersonnel_criteria.mRclvdDetailId = rclvd_detail_id;
            RovingCheckListViolationPersonnel_criteria.mRovingChecklistViolationId = roving_checklist_violation_id;
            RovingCheckListViolationPersonnel_criteria.mEmployeeId = employee_id;

            RovingCheckListViolationPersonnel_list = RovingCheckListViolationPersonnelManager.GetList(RovingCheckListViolationPersonnel_criteria);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RovingCheckListViolationPersonnel_list.Count, rovingchecklistviolationpersonnels = RovingCheckListViolationPersonnel_list }, Configuration.Formatters.JsonFormatter);
		}

		// POST: api/RovingCheckListViolationPersonnel
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(RovingCheckListViolationPersonnel RovingCheckListViolationPersonnel)
		{
			if (RovingCheckListViolationPersonnel.Validate())
			{
				int result;

				result = RovingCheckListViolationPersonnelManager.Save(RovingCheckListViolationPersonnel);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/RovingCheckListViolationPersonnel/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RovingCheckListViolationPersonnel/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RovingCheckListViolationPersonnel RovingCheckListViolationPersonnel)
		{
			try
			{
				int result;

				result = RovingCheckListViolationPersonnelManager.Delete(RovingCheckListViolationPersonnel);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}