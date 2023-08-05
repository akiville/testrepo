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
	public class RovingPlanOicController : ApiController
	{
		// GET: api/RovingPlanOic
		public HttpResponseMessage Get(int id)
        {
            RovingPlanOicCollection RovingPlanOic_list = new RovingPlanOicCollection();
            RovingPlanOic rovingPlanOic = new RovingPlanOic();

            rovingPlanOic = RovingPlanOicManager.GetItem(id);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = RovingPlanOic_list.Count, rovingplanoics = RovingPlanOic_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/RovingPlanOic/5
        public HttpResponseMessage Get(int roving_plan_id, int employee_id)
		{
			RovingPlanOicCollection RovingPlanOic_list = new RovingPlanOicCollection();
			RovingPlanOicCriteria RovingPlanOic_criteria = new RovingPlanOicCriteria();

            RovingPlanOic_criteria.mRovingPlanId = roving_plan_id;
            RovingPlanOic_criteria.mEmployeeId = employee_id;

            RovingPlanOic_list = RovingPlanOicManager.GetList(RovingPlanOic_criteria);
			
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RovingPlanOic_list.Count, rovingplanoics = RovingPlanOic_list }, Configuration.Formatters.JsonFormatter);
				
			
		}

		// POST: api/RovingPlanOic
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(RovingPlanOic RovingPlanOic)
		{
			if (RovingPlanOic.Validate())
			{
				int result;

				result = RovingPlanOicManager.Save(RovingPlanOic);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/RovingPlanOic/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RovingPlanOic/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RovingPlanOic RovingPlanOic)
		{
			try
			{
				int result;

				result = RovingPlanOicManager.Delete(RovingPlanOic);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}