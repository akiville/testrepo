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
	public class TrainingAttendanceDetailsController : ApiController
	{
		// GET: api/TrainingAttendanceDetails
		public HttpResponseMessage Get(int id)
		{
            TrainingAttendanceDetailsCollection TrainingAttendanceDetails_list = new TrainingAttendanceDetailsCollection();
            TrainingAttendanceDetails training_attendance_detail = new TrainingAttendanceDetails();
            training_attendance_detail = TrainingAttendanceDetailsManager.GetItem(id);
            TrainingAttendanceDetails_list.Add(training_attendance_detail);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = TrainingAttendanceDetails_list.Count, trainingattendancedetailss = TrainingAttendanceDetails_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/TrainingAttendanceDetails/5
        public HttpResponseMessage Get(int training_attendance_id, int lmm_id, String date_created)
		{
			
			TrainingAttendanceDetailsCollection TrainingAttendanceDetails_list = new TrainingAttendanceDetailsCollection();
			TrainingAttendanceDetailsCriteria TrainingAttendanceDetails_criteria = new TrainingAttendanceDetailsCriteria();

            TrainingAttendanceDetails_criteria.mTrainingAttendanceId = training_attendance_id;
            TrainingAttendanceDetails_criteria.mLmmId = lmm_id;
            TrainingAttendanceDetails_criteria.mDateCreated = date_created;

            TrainingAttendanceDetails_list = TrainingAttendanceDetailsManager.GetList(TrainingAttendanceDetails_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  TrainingAttendanceDetails_list.Count, trainingattendancedetailss = TrainingAttendanceDetails_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/TrainingAttendanceDetails
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(TrainingAttendanceDetails TrainingAttendanceDetails)
		{
			if (TrainingAttendanceDetails.Validate())
			{
				int result;

				result = TrainingAttendanceDetailsManager.Save(TrainingAttendanceDetails);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/TrainingAttendanceDetails/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/TrainingAttendanceDetails/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(TrainingAttendanceDetails TrainingAttendanceDetails)
		{
			try
			{
				int result;

				result = TrainingAttendanceDetailsManager.Delete(TrainingAttendanceDetails);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}