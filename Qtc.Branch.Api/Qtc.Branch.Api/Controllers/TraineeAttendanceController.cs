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
	public class TraineeAttendanceController : ApiController
	{
        // GET: api/TraineeAttendance
        public HttpResponseMessage Get(int id)
        {
            TraineeAttendanceCollection TraineeAttendance_list = new TraineeAttendanceCollection();
            TraineeAttendance traineeAttendance = new TraineeAttendance();

            traineeAttendance = TraineeAttendanceManager.GetItem(id);
            TraineeAttendance_list.Add(traineeAttendance);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = TraineeAttendance_list.Count, traineeattendances = TraineeAttendance_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/TraineeAttendance/5
        public HttpResponseMessage Get(int lmm_id, int employee_id, DateTime date, int branch_id)
		{
			//try
			//{
				TraineeAttendanceCollection TraineeAttendance_list = new TraineeAttendanceCollection();
				TraineeAttendanceCriteria TraineeAttendance_criteria = new TraineeAttendanceCriteria();

                TraineeAttendance_criteria.mLmmId = lmm_id;
                TraineeAttendance_criteria.mEmployeeId = employee_id;
                TraineeAttendance_criteria.mDate = date;
                TraineeAttendance_criteria.mBranchId = branch_id;

                TraineeAttendance_list = TraineeAttendanceManager.GetList(TraineeAttendance_criteria);
				//if (TraineeAttendance_list.Count > 0 ) 
				//{
					return Request.CreateResponse(HttpStatusCode.OK, new { count =  TraineeAttendance_list.Count, traineeattendances = TraineeAttendance_list }, Configuration.Formatters.JsonFormatter);
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

		// POST: api/TraineeAttendance
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(TraineeAttendance TraineeAttendance)
		{
			if (TraineeAttendance.Validate())
			{
				int result;

				result = TraineeAttendanceManager.Save(TraineeAttendance);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/TraineeAttendance/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/TraineeAttendance/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(TraineeAttendance TraineeAttendance)
		{
			try
			{
				int result;

				result = TraineeAttendanceManager.Delete(TraineeAttendance);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}