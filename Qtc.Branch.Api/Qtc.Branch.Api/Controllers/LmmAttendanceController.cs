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
	public class LmmAttendanceController : ApiController
	{
        // GET: api/LmmAttendance
        public HttpResponseMessage Get( int id_value)
		{
            try
            {
                LmmAttendanceCollection LmmAttendance_list = new LmmAttendanceCollection();

                LmmAttendance lmmAttendance = new LmmAttendance();

                lmmAttendance = LmmAttendanceManager.GetItem(id_value);

                LmmAttendance_list.Add(lmmAttendance);

                return Request.CreateResponse(HttpStatusCode.OK, new { count = LmmAttendance_list.Count, lmmattendances = LmmAttendance_list }, Configuration.Formatters.JsonFormatter);
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
            }
        }

		// GET: api/LmmAttendance
		public HttpResponseMessage Get(int lmm_id, int employee_id, DateTime attendance_date)
		{
			//try
			//{
			LmmAttendanceCollection LmmAttendance_list = new LmmAttendanceCollection();
			LmmAttendanceCriteria LmmAttendance_criteria = new LmmAttendanceCriteria();

            LmmAttendance_criteria.mLmmId = lmm_id;
            LmmAttendance_criteria.mEmployeeId = employee_id;
            LmmAttendance_criteria.mAttendanceDate = attendance_date;


            LmmAttendance_list = LmmAttendanceManager.GetList(LmmAttendance_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  LmmAttendance_list.Count, lmmattendances = LmmAttendance_list }, Configuration.Formatters.JsonFormatter);
			
		}

        public HttpResponseMessage GetCount(int lmm_id, int employee_id, DateTime start_date, DateTime end_date)
        {
            //try
            //{
            LmmAttendanceCollection LmmAttendance_list = new LmmAttendanceCollection();
            LmmAttendanceCriteria LmmAttendance_criteria = new LmmAttendanceCriteria();

            LmmAttendance_criteria.mLmmId = lmm_id;
            LmmAttendance_criteria.mEmployeeId = employee_id;
            LmmAttendance_criteria.mStartDate = start_date;
            LmmAttendance_criteria.mEndDate = end_date;

            LmmAttendance_list = LmmAttendanceManager.GetListCount(LmmAttendance_criteria);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = LmmAttendance_list.Count, lmmattendances = LmmAttendance_list }, Configuration.Formatters.JsonFormatter);

        }



        // POST: api/LmmAttendance
        //[HttpPost]
        //[CustomAuthorizationFilter]
        public HttpResponseMessage Post(LmmAttendance LmmAttendance)
		{
			if (LmmAttendance.Validate())
			{
				int result;

				result = LmmAttendanceManager.Save(LmmAttendance);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/LmmAttendance/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/LmmAttendance/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(LmmAttendance LmmAttendance)
		{
			try
			{
				int result;

				result = LmmAttendanceManager.Delete(LmmAttendance);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}