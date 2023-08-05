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
	public class NotifyLmmForTransferedSalesEmployeeController : ApiController
	{
        // GET: api/NotifyLmmForTransferedSalesEmployee
        public HttpResponseMessage Get(int id)
        {
            NotifyLmmForTransferedSalesEmployeeCollection NotifyLmmForTransferedSalesEmployee_list = new NotifyLmmForTransferedSalesEmployeeCollection();
            NotifyLmmForTransferedSalesEmployee NotifyLmmForTransferedSalesEmployee = new NotifyLmmForTransferedSalesEmployee();
            NotifyLmmForTransferedSalesEmployee = NotifyLmmForTransferedSalesEmployeeManager.GetItem(id);
            NotifyLmmForTransferedSalesEmployee_list.Add(NotifyLmmForTransferedSalesEmployee);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = NotifyLmmForTransferedSalesEmployee_list.Count, notifylmmfortransferedsalesemployees = NotifyLmmForTransferedSalesEmployee_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/NotifyLmmForTransferedSalesEmployee/5
        public HttpResponseMessage Get(int lmm_from_id, int lmm_to_id, DateTime start_date, DateTime end_date )
		{
			NotifyLmmForTransferedSalesEmployeeCollection NotifyLmmForTransferedSalesEmployee_list = new NotifyLmmForTransferedSalesEmployeeCollection();
			NotifyLmmForTransferedSalesEmployeeCriteria NotifyLmmForTransferedSalesEmployee_criteria = new NotifyLmmForTransferedSalesEmployeeCriteria();

            NotifyLmmForTransferedSalesEmployee_criteria.mLmmToId = lmm_to_id;
            NotifyLmmForTransferedSalesEmployee_criteria.mLmmFromId = lmm_from_id;
            NotifyLmmForTransferedSalesEmployee_criteria.mStartDate = start_date;
            NotifyLmmForTransferedSalesEmployee_criteria.mEndDate = end_date;

            NotifyLmmForTransferedSalesEmployee_list = NotifyLmmForTransferedSalesEmployeeManager.GetList(NotifyLmmForTransferedSalesEmployee_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  NotifyLmmForTransferedSalesEmployee_list.Count, notifylmmfortransferedsalesemployees = NotifyLmmForTransferedSalesEmployee_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/NotifyLmmForTransferedSalesEmployee
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(NotifyLmmForTransferedSalesEmployee NotifyLmmForTransferedSalesEmployee)
		{
			if (NotifyLmmForTransferedSalesEmployee.Validate())
			{
				int result;

				result = NotifyLmmForTransferedSalesEmployeeManager.Save(NotifyLmmForTransferedSalesEmployee);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/NotifyLmmForTransferedSalesEmployee/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/NotifyLmmForTransferedSalesEmployee/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(NotifyLmmForTransferedSalesEmployee NotifyLmmForTransferedSalesEmployee)
		{
			try
			{
				int result;

				result = NotifyLmmForTransferedSalesEmployeeManager.Delete(NotifyLmmForTransferedSalesEmployee);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}