using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Collections.Generic;
using Qtc.Branch.Bll;
using Qtc.Branch.BusinessEntities;
using System.IO;

namespace Qtc.Branch.Api.Controllers
{
	public class EmployeeController : ApiController
	{
		// GET: api/Employee
		public IEnumerable<string> Get()
		{
			return new string[] { "value1", "value2" };
		}

		// GET: api/Employee
		 public HttpResponseMessage Get(int  tracking_no, int branch_code, String password)
		{
			
				EmployeeCollection Employee_list = new EmployeeCollection();
				EmployeeCriteria Employee_criteria = new EmployeeCriteria();

                Employee_criteria.mTrackingNo = tracking_no;
                Employee_criteria.mBranchCode = branch_code;
                Employee_criteria.mPassword = password;

                Employee_list = EmployeeManager.GetList(Employee_criteria);
				
				return Request.CreateResponse(HttpStatusCode.OK, new { count = Employee_list.Count, employees = Employee_list }, Configuration.Formatters.JsonFormatter);
                
			//}
			//catch (IOException e)
   //         {
			//	return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error :" + e );
			//}
		}

        public HttpResponseMessage Get(int tracking_no, int branch_code, String password, DateTime sales_schedule_date)
        {

            EmployeeCollection Employee_list = new EmployeeCollection();
            EmployeeCriteria Employee_criteria = new EmployeeCriteria();

            Employee_criteria.mTrackingNo = tracking_no;
            Employee_criteria.mBranchCode = branch_code;
            Employee_criteria.mPassword = password;
            Employee_criteria.mSalesScheduleDate = sales_schedule_date;

            Employee_list = EmployeeManager.GetListWithDate(Employee_criteria);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = Employee_list.Count, employees = Employee_list }, Configuration.Formatters.JsonFormatter);

            //}
            //catch (IOException e)
            //         {
            //	return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error :" + e );
            //}
        }



        // POST: api/Employee
        //[HttpPost]
        //[CustomAuthorizationFilter]
        public HttpResponseMessage Post(Employee Employee)
		{
			if (Employee.Validate())
			{
				int result;

				result = EmployeeManager.Save(Employee);

				return Request.CreateResponse(HttpStatusCode.OK, result); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/Employee/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/Employee/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(Employee Employee)
		{
			try
			{
				int result;

				result = EmployeeManager.Delete(Employee);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}