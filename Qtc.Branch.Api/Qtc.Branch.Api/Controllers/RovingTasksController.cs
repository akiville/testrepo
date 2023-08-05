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
	public class RovingTasksController : ApiController
	{
		// GET: api/RovingTasks
		public HttpResponseMessage Get(int id)
        {
            RovingTasksCollection RovingTasks_list = new RovingTasksCollection();
            RovingTasks roving_task = new RovingTasks();
            roving_task = RovingTasksManager.GetItem(id);
            RovingTasks_list.Add(roving_task);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = RovingTasks_list.Count, rovingtaskss = RovingTasks_list }, Configuration.Formatters.JsonFormatter);
        }

		// GET: api/RovingTasks/5
		 public HttpResponseMessage Get()
		{
			
			RovingTasksCollection RovingTasks_list = new RovingTasksCollection();
			RovingTasksCriteria RovingTasks_criteria = new RovingTasksCriteria();

			RovingTasks_list = RovingTasksManager.GetList(RovingTasks_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RovingTasks_list.Count, rovingtaskss = RovingTasks_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/RovingTasks
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(RovingTasks RovingTasks)
		{
			if (RovingTasks.Validate())
			{
				int result;

				result = RovingTasksManager.Save(RovingTasks);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/RovingTasks/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RovingTasks/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RovingTasks RovingTasks)
		{
			try
			{
				int result;

				result = RovingTasksManager.Delete(RovingTasks);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}