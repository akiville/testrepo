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
	public class ReasonController : ApiController
	{
		// GET: api/Reason
		 public HttpResponseMessage Get(int id)
		{
			ReasonCollection Reason_list = new ReasonCollection();
			Reason Reason = new Reason();
			Reason = ReasonManager.GetItem(id);
			Reason_list.Add(Reason);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  Reason_list.Count, reasons = Reason_list }, Configuration.Formatters.JsonFormatter);
		}

		// GET: api/Reason/5
		 public HttpResponseMessage Get(Boolean repair, Boolean logistics)
		{
			ReasonCollection Reason_list = new ReasonCollection();
			ReasonCriteria Reason_criteria = new ReasonCriteria();
            Reason_criteria.mRepair = repair;
            Reason_criteria.mLogistics = logistics;
            Reason_list = ReasonManager.GetList(Reason_criteria);
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  Reason_list.Count, reasons = Reason_list }, Configuration.Formatters.JsonFormatter);
		}

		// POST: api/Reason
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(Reason Reason)
		{
			 if (Reason.Validate())
			{
				int result;

				result = ReasonManager.Save(Reason);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/Reason/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/Reason/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(Reason Reason)
		{
			try
			{
				int result;

				result = ReasonManager.Delete(Reason);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}