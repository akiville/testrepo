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
	public class LmmAttendanceUpdateController : ApiController
	{
        // GET: api/LmmAttendanceUpdate
        public HttpResponseMessage Get(int id)
        {
            LmmAttendanceUpdateCollection LmmAttendanceUpdate_list = new LmmAttendanceUpdateCollection();
            LmmAttendanceUpdate lmmAttendanceUpdate = new LmmAttendanceUpdate();
            lmmAttendanceUpdate = LmmAttendanceUpdateManager.GetItem(id);
            LmmAttendanceUpdate_list.Add(lmmAttendanceUpdate);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = LmmAttendanceUpdate_list.Count, lmmattendanceupdates = LmmAttendanceUpdate_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/LmmAttendanceUpdate/5
        public HttpResponseMessage Get(int lmm_id, int employee_id, int cutoff_id)
		{
			
			LmmAttendanceUpdateCollection LmmAttendanceUpdate_list = new LmmAttendanceUpdateCollection();
			LmmAttendanceUpdateCriteria LmmAttendanceUpdate_criteria = new LmmAttendanceUpdateCriteria();

            LmmAttendanceUpdate_criteria.mLmmId = lmm_id;
            LmmAttendanceUpdate_criteria.mEmployeeId = employee_id;
            LmmAttendanceUpdate_criteria.mCutoffId = cutoff_id;

            LmmAttendanceUpdate_list = LmmAttendanceUpdateManager.GetList(LmmAttendanceUpdate_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  LmmAttendanceUpdate_list.Count, lmmattendanceupdates = LmmAttendanceUpdate_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/LmmAttendanceUpdate
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(LmmAttendanceUpdate LmmAttendanceUpdate)
		{
			if (LmmAttendanceUpdate.Validate())
			{
				int result;

				result = LmmAttendanceUpdateManager.Save(LmmAttendanceUpdate);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/LmmAttendanceUpdate/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/LmmAttendanceUpdate/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(LmmAttendanceUpdate LmmAttendanceUpdate)
		{
			try
			{
				int result;

				result = LmmAttendanceUpdateManager.Delete(LmmAttendanceUpdate);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}