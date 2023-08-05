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
	public class EmployeeHrLetterController : ApiController
	{
		// GET: api/EmployeeHrLetter
		public HttpResponseMessage Get(int id)
        {
            EmployeeHrLetterCollection EmployeeHrLetter_list = new EmployeeHrLetterCollection();
            EmployeeHrLetter employee_hr_letter = new EmployeeHrLetter();
            employee_hr_letter = EmployeeHrLetterManager.GetItem(id);
            EmployeeHrLetter_list.Add(employee_hr_letter);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = EmployeeHrLetter_list.Count, employeehrletters = EmployeeHrLetter_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/EmployeeHrLetter/5
        public HttpResponseMessage Get(DateTime duration_from, DateTime duration_to, int request_by, int employee_id, int branch_id, int branch_to )
		{
			
			EmployeeHrLetterCollection EmployeeHrLetter_list = new EmployeeHrLetterCollection();
			EmployeeHrLetterCriteria EmployeeHrLetter_criteria = new EmployeeHrLetterCriteria();

            EmployeeHrLetter_criteria.mDurationFrom = duration_from;
            EmployeeHrLetter_criteria.mDurationTo = duration_to;
            EmployeeHrLetter_criteria.mRequestBy = request_by;
            EmployeeHrLetter_criteria.mEmployeeId = employee_id;
            EmployeeHrLetter_criteria.mBranchId = branch_id;
            EmployeeHrLetter_criteria.mBranchTo = branch_to;

            EmployeeHrLetter_list = EmployeeHrLetterManager.GetList(EmployeeHrLetter_criteria);
				

			return Request.CreateResponse(HttpStatusCode.OK, new { count =  EmployeeHrLetter_list.Count, employeehrletters = EmployeeHrLetter_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/EmployeeHrLetter
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(EmployeeHrLetter EmployeeHrLetter)
		{
			if (EmployeeHrLetter.Validate())
			{
				int result;

				result = EmployeeHrLetterManager.Save(EmployeeHrLetter);

				return Request.CreateResponse(HttpStatusCode.OK, result); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/EmployeeHrLetter/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/EmployeeHrLetter/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(EmployeeHrLetter EmployeeHrLetter)
		{
			try
			{
				int result;

				result = EmployeeHrLetterManager.Delete(EmployeeHrLetter);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}