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
	public class LogsController : ApiController
	{
		// GET: api/Logs
		//public IEnumerable<string> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		// GET: api/Logs/5
		 public HttpResponseMessage Get()
		{
			try
			{
				LogsCollection Logs_list = new LogsCollection();
				LogsCriteria Logs_criteria = new LogsCriteria();

				Logs_list = LogsManager.GetList(Logs_criteria);
				if (Logs_list.Count > 0 ) 
				{
					return Request.CreateResponse(HttpStatusCode.OK, new { count =  Logs_list.Count, logss = Logs_list }, Configuration.Formatters.JsonFormatter);
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

		// POST: api/Logs
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(Logs Logs)
		{
			if (Logs.Validate())
			{
				int result;

				result = LogsManager.Save(Logs);

                return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/Logs/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/Logs/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(Logs Logs)
		{
			try
			{
				int result;

				result = LogsManager.Delete(Logs);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}