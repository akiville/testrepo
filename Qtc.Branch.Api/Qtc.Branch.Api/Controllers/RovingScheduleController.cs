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
	public class RovingScheduleController : ApiController
	{
        // GET: api/RovingSchedule
        public HttpResponseMessage Get(int id)
        {
            RovingScheduleCollection RovingSchedule_list = new RovingScheduleCollection();
            RovingSchedule roving_Schedule = new RovingSchedule();
            roving_Schedule = RovingScheduleManager.GetItem(id);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = RovingSchedule_list.Count, rovingschedules = RovingSchedule_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/RovingSchedule/5
        public HttpResponseMessage Get()
		{
			
			RovingScheduleCollection RovingSchedule_list = new RovingScheduleCollection();
			RovingScheduleCriteria RovingSchedule_criteria = new RovingScheduleCriteria();

			RovingSchedule_list = RovingScheduleManager.GetList(RovingSchedule_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RovingSchedule_list.Count, rovingschedules = RovingSchedule_list }, Configuration.Formatters.JsonFormatter);
			
				
		}

		// POST: api/RovingSchedule
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(RovingSchedule RovingSchedule)
		{
			if (RovingSchedule.Validate())
			{
				int result;

				result = RovingScheduleManager.Save(RovingSchedule);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/RovingSchedule/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RovingSchedule/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RovingSchedule RovingSchedule)
		{
			try
			{
				int result;

				result = RovingScheduleManager.Delete(RovingSchedule);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}