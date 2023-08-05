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
	public class EmployeeStatusEmploymentController : ApiController
	{
        // GET: api/EmployeeStatusEmployment
        public HttpResponseMessage Get(int id)
        {
            EmployeeStatusEmploymentCollection EmployeeStatusEmployment_list = new EmployeeStatusEmploymentCollection();
            EmployeeStatusEmployment employeeStatusEmployement;

            employeeStatusEmployement = EmployeeStatusEmploymentManager.GetItem(id);
            EmployeeStatusEmployment_list.Add(employeeStatusEmployement);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = EmployeeStatusEmployment_list.Count, employeestatusemployments = EmployeeStatusEmployment_list }, Configuration.Formatters.JsonFormatter);
            
        }

        // GET: api/EmployeeStatusEmployment/5
        public HttpResponseMessage Get()
		{
			
			EmployeeStatusEmploymentCollection EmployeeStatusEmployment_list = new EmployeeStatusEmploymentCollection();
			EmployeeStatusEmploymentCriteria EmployeeStatusEmployment_criteria = new EmployeeStatusEmploymentCriteria();

			EmployeeStatusEmployment_list = EmployeeStatusEmploymentManager.GetList(EmployeeStatusEmployment_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  EmployeeStatusEmployment_list.Count, employeestatusemployments = EmployeeStatusEmployment_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/EmployeeStatusEmployment
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(EmployeeStatusEmployment EmployeeStatusEmployment)
		{
			if (EmployeeStatusEmployment.Validate())
			{
				int result;

				result = EmployeeStatusEmploymentManager.Save(EmployeeStatusEmployment);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/EmployeeStatusEmployment/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/EmployeeStatusEmployment/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(EmployeeStatusEmployment EmployeeStatusEmployment)
		{
			try
			{
				int result;

				result = EmployeeStatusEmploymentManager.Delete(EmployeeStatusEmployment);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}