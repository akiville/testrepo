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
	public class TrainingAttendanceController : ApiController
	{
		// GET: api/TrainingAttendance
		public HttpResponseMessage Get(int id)
		{
            TrainingAttendanceCollection TrainingAttendance_list = new TrainingAttendanceCollection();
            TrainingAttendance training_attendance = new TrainingAttendance();

            training_attendance = TrainingAttendanceManager.GetItem(id);

            TrainingAttendance_list.Add(training_attendance);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = TrainingAttendance_list.Count, trainingattendances = TrainingAttendance_list }, Configuration.Formatters.JsonFormatter);
        }

		// GET: api/TrainingAttendance/5
		 public HttpResponseMessage Get(int lmm_id, DateTime date_created)
		{
			//try
			//{
				TrainingAttendanceCollection TrainingAttendance_list = new TrainingAttendanceCollection();
				TrainingAttendanceCriteria TrainingAttendance_criteria = new TrainingAttendanceCriteria();

                TrainingAttendance_criteria.mLmmId = lmm_id;
                TrainingAttendance_criteria.mDateCreated = date_created;

                TrainingAttendance_list = TrainingAttendanceManager.GetList(TrainingAttendance_criteria);
				//if (TrainingAttendance_list.Count > 0 ) 
				//{
				return Request.CreateResponse(HttpStatusCode.OK, new { count =  TrainingAttendance_list.Count, trainingattendances = TrainingAttendance_list }, Configuration.Formatters.JsonFormatter);
			//	}
			//	else
			//	{
			//		return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
			//	}
			//}
			//catch
			//{
			//	return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			//}
		}

		// POST: api/TrainingAttendance
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(TrainingAttendance TrainingAttendance)
		{
			if (TrainingAttendance.Validate())
			{
				int result;

				result = TrainingAttendanceManager.Save(TrainingAttendance);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/TrainingAttendance/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/TrainingAttendance/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(TrainingAttendance TrainingAttendance)
		{
			try
			{
				int result;

				result = TrainingAttendanceManager.Delete(TrainingAttendance);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}