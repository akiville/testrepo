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
	public class LmmAttendanceScheduleTypeController : ApiController
	{
		// GET: api/LmmAttendanceScheduleType
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET: api/LmmAttendanceScheduleType/5
		 public HttpResponseMessage Get(int for_urgent_schedule_changed )
		{
			//try
			//{
				LmmAttendanceScheduleTypeCollection LmmAttendanceScheduleType_list = new LmmAttendanceScheduleTypeCollection();
				LmmAttendanceScheduleTypeCriteria LmmAttendanceScheduleType_criteria = new LmmAttendanceScheduleTypeCriteria();

                LmmAttendanceScheduleType_criteria.mForUrgentScheduleChanged = for_urgent_schedule_changed;

                LmmAttendanceScheduleType_list = LmmAttendanceScheduleTypeManager.GetList(LmmAttendanceScheduleType_criteria);
				
				return Request.CreateResponse(HttpStatusCode.OK, new { count =  LmmAttendanceScheduleType_list.Count, lmmattendancescheduletypes = LmmAttendanceScheduleType_list }, Configuration.Formatters.JsonFormatter);
				
			//}
			//catch
			//{
			//	return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			//}
		}

		// POST: api/LmmAttendanceScheduleType
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(LmmAttendanceScheduleType LmmAttendanceScheduleType)
		{
			if (LmmAttendanceScheduleType.Validate())
			{
				int result;

				result = LmmAttendanceScheduleTypeManager.Save(LmmAttendanceScheduleType);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/LmmAttendanceScheduleType/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/LmmAttendanceScheduleType/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(LmmAttendanceScheduleType LmmAttendanceScheduleType)
		{
			try
			{
				int result;

				result = LmmAttendanceScheduleTypeManager.Delete(LmmAttendanceScheduleType);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}