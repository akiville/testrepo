using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using Qtc.Branch.Bll;
using Qtc.Branch.BusinessEntities;
using Qtc.Branch.Api.Models;

namespace Qtc.Branch.Api.Controllers
{
	public class EmployeeAttendanceController : ApiController
	{
		// GET: api/EmployeeAttendance
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET: api/EmployeeAttendance/5
		 public HttpResponseMessage Get(int id)
		{
			try
			{
				EmployeeAttendanceCollection EmployeeAttendance_list = new EmployeeAttendanceCollection();
				EmployeeAttendanceCriteria EmployeeAttendance_criteria = new EmployeeAttendanceCriteria();

				EmployeeAttendance_list = EmployeeAttendanceManager.GetList(EmployeeAttendance_criteria);
				if (EmployeeAttendance_list.Count > 0 ) 
				{
					return Request.CreateResponse(HttpStatusCode.OK, EmployeeAttendance_list);
				}
				else
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
				}
			}
			catch
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			}
		}

        // POST: api/EmployeeAttendance
        //[HttpPost]
        //[CustomAuthorizationFilter]
        public HttpResponseMessage Post(EmployeeAttendanceApi EmployeeAttendanceApi)
		{
            EmployeeAttendance EmployeeAttendance = new EmployeeAttendance();

            EmployeeAttendance.mId = EmployeeAttendanceApi.Id;
            EmployeeAttendance.mEmployeeId = EmployeeAttendanceApi.EmployeeId;
            EmployeeAttendance.mAttendanceId = EmployeeAttendanceApi.AttendanceId;
            EmployeeAttendance.mBranchCode = EmployeeAttendanceApi.BranchCode;
            EmployeeAttendance.mAttendanceTrackingNo = EmployeeAttendanceApi.AttendanceTrackingNo;
            EmployeeAttendance.mAttendanceDate = Convert.ToDateTime(EmployeeAttendanceApi.AttendanceDate);
            EmployeeAttendance.mUserFullName = "";
            EmployeeAttendance.mUserId = 0;

            if (EmployeeAttendance.Validate())
			{
               
				int result;

				result = EmployeeAttendanceManager.Save(EmployeeAttendance);

				return Request.CreateResponse(HttpStatusCode.OK, result); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/EmployeeAttendance/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/EmployeeAttendance/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(EmployeeAttendance EmployeeAttendance)
		{
			try
			{
				int result;

				result = EmployeeAttendanceManager.Delete(EmployeeAttendance);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}