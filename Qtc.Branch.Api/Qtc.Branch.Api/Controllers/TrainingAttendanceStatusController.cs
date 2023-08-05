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
	public class TrainingAttendanceStatusController : ApiController
	{
		// GET: api/TrainingAttendanceStatus
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET: api/TrainingAttendanceStatus/5
		 public HttpResponseMessage Get()
		{
			//try
			//{
				TrainingAttendanceStatusCollection TrainingAttendanceStatus_list = new TrainingAttendanceStatusCollection();
				TrainingAttendanceStatusCriteria TrainingAttendanceStatus_criteria = new TrainingAttendanceStatusCriteria();

				TrainingAttendanceStatus_list = TrainingAttendanceStatusManager.GetList(TrainingAttendanceStatus_criteria);
				//if (TrainingAttendanceStatus_list.Count > 0 ) 
				//{
					return Request.CreateResponse(HttpStatusCode.OK, new { count =  TrainingAttendanceStatus_list.Count, trainingattendancestatuss = TrainingAttendanceStatus_list }, Configuration.Formatters.JsonFormatter);
				//}
				//else
				//{
				//	return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
				//}
			//}
			//catch
			//{
			//	return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			//}
		}

		// POST: api/TrainingAttendanceStatus
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(TrainingAttendanceStatus TrainingAttendanceStatus)
		{
			if (TrainingAttendanceStatus.Validate())
			{
				int result;

				result = TrainingAttendanceStatusManager.Save(TrainingAttendanceStatus);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/TrainingAttendanceStatus/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/TrainingAttendanceStatus/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(TrainingAttendanceStatus TrainingAttendanceStatus)
		{
			try
			{
				int result;

				result = TrainingAttendanceStatusManager.Delete(TrainingAttendanceStatus);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}