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
	public class DtrLogsOvertimeReasonController : ApiController
	{
        // GET: api/DtrLogsOvertimeReason
        public HttpResponseMessage Get(int id)
        {
            DtrLogsOvertimeReasonCollection DtrLogsOvertimeReason_list = new DtrLogsOvertimeReasonCollection();
            DtrLogsOvertimeReason dtrLogsOvertimeReason = new DtrLogsOvertimeReason();
            dtrLogsOvertimeReason = DtrLogsOvertimeReasonManager.GetItem(id);
            DtrLogsOvertimeReason_list.Add(dtrLogsOvertimeReason);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = DtrLogsOvertimeReason_list.Count, dtrlogsovertimereasons = DtrLogsOvertimeReason_list }, Configuration.Formatters.JsonFormatter);
        }

		// GET: api/DtrLogsOvertimeReason/5
		public HttpResponseMessage Get()
		{
			
			DtrLogsOvertimeReasonCollection DtrLogsOvertimeReason_list = new DtrLogsOvertimeReasonCollection();
			DtrLogsOvertimeReasonCriteria DtrLogsOvertimeReason_criteria = new DtrLogsOvertimeReasonCriteria();

			DtrLogsOvertimeReason_list = DtrLogsOvertimeReasonManager.GetList(DtrLogsOvertimeReason_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  DtrLogsOvertimeReason_list.Count, dtrlogsovertimereasons = DtrLogsOvertimeReason_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/DtrLogsOvertimeReason
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(DtrLogsOvertimeReason DtrLogsOvertimeReason)
		{
			if (DtrLogsOvertimeReason.Validate())
			{
				int result;

				result = DtrLogsOvertimeReasonManager.Save(DtrLogsOvertimeReason);

				return Request.CreateResponse(HttpStatusCode.OK, result); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/DtrLogsOvertimeReason/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/DtrLogsOvertimeReason/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(DtrLogsOvertimeReason DtrLogsOvertimeReason)
		{
			try
			{
				int result;

				result = DtrLogsOvertimeReasonManager.Delete(DtrLogsOvertimeReason);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}