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
	public class MessengerStatusController : ApiController
	{
		// GET: api/MessengerStatus
		public HttpResponseMessage Get(int id)
		{
            MessengerStatusCollection MessengerStatus_list = new MessengerStatusCollection();
            MessengerStatus messenger_status = new MessengerStatus();

            messenger_status = MessengerStatusManager.GetItem(id);
            MessengerStatus_list.Add(messenger_status);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = MessengerStatus_list.Count, messengerstatuss = MessengerStatus_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/MessengerStatus/5
        public HttpResponseMessage Get(int messenger_detail_id, int employee_id)
		{
			MessengerStatusCollection MessengerStatus_list = new MessengerStatusCollection();
			MessengerStatusCriteria MessengerStatus_criteria = new MessengerStatusCriteria();

            MessengerStatus_criteria.mMessengerDetailId = messenger_detail_id;
            MessengerStatus_criteria.mEmployeeId = employee_id;

            MessengerStatus_list = MessengerStatusManager.GetList(MessengerStatus_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  MessengerStatus_list.Count, messengerstatuss = MessengerStatus_list }, Configuration.Formatters.JsonFormatter);
	
		}

		// POST: api/MessengerStatus
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(MessengerStatus MessengerStatus)
		{
			if (MessengerStatus.Validate())
			{
				int result;

				result = MessengerStatusManager.Save(MessengerStatus);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/MessengerStatus/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/MessengerStatus/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(MessengerStatus MessengerStatus)
		{
			try
			{
				int result;

				result = MessengerStatusManager.Delete(MessengerStatus);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}