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
	public class UrgentScheduleChangeController : ApiController
	{
		// GET: api/UrgentScheduleChange/5
		public HttpResponseMessage Get(int id)
		{
            UrgentScheduleChangeCollection UrgentScheduleChange_list = new UrgentScheduleChangeCollection();
            UrgentScheduleChange urgentScheduleChange;

            urgentScheduleChange = UrgentScheduleChangeManager.GetItem(id);

            UrgentScheduleChange_list.Add(urgentScheduleChange);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = 1, urgentschedulechanges = UrgentScheduleChange_list }, Configuration.Formatters.JsonFormatter);
        }

		// GET: api/UrgentScheduleChange
		 public HttpResponseMessage Get(int lmm_id, int affected_branch_id, DateTime start_date, DateTime end_date, int affected_personnel_id, DateTime date_filed, int to_lmm_id, int employee_id)
		{
			//try
			//{
			UrgentScheduleChangeCollection UrgentScheduleChange_list = new UrgentScheduleChangeCollection();
			UrgentScheduleChangeCriteria UrgentScheduleChange_criteria = new UrgentScheduleChangeCriteria();

            UrgentScheduleChange_criteria.mLmmId = lmm_id;
            UrgentScheduleChange_criteria.mStartDate = start_date;
            UrgentScheduleChange_criteria.mEndDate = end_date;
            UrgentScheduleChange_criteria.mAffectedPersonnelId = affected_personnel_id;
            UrgentScheduleChange_criteria.mDateFiled = date_filed;
            UrgentScheduleChange_criteria.mToLmmId = to_lmm_id;
            UrgentScheduleChange_criteria.mEmployeeId = employee_id;

            UrgentScheduleChange_list = UrgentScheduleChangeManager.GetList(UrgentScheduleChange_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  UrgentScheduleChange_list.Count, urgentschedulechanges = UrgentScheduleChange_list }, Configuration.Formatters.JsonFormatter);
				
			//}
			//catch
			//{
			//	return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			//}
		}

		// POST: api/UrgentScheduleChange
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(UrgentScheduleChange UrgentScheduleChange)
		{
			if (UrgentScheduleChange.Validate())
			{
				int result;

				result = UrgentScheduleChangeManager.Save(UrgentScheduleChange);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/UrgentScheduleChange/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/UrgentScheduleChange/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(UrgentScheduleChange UrgentScheduleChange)
		{
			try
			{
				int result;

				result = UrgentScheduleChangeManager.Delete(UrgentScheduleChange);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}