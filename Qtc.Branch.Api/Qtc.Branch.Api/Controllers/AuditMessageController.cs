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
	public class AuditMessageController : ApiController
	{
        // GET: api/AuditMessage
        public HttpResponseMessage Get(int id)
        {
            AuditMessageCollection AuditMessage_list = new AuditMessageCollection();
            AuditMessage audit_message;
            audit_message = AuditMessageManager.GetItem(id);
            AuditMessage_list.Add(audit_message);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = AuditMessage_list.Count, auditmessages = AuditMessage_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/AuditMessage/5
        public HttpResponseMessage Get(int user_id, String status)
		{
			
			AuditMessageCollection AuditMessage_list = new AuditMessageCollection();
			AuditMessageCriteria AuditMessage_criteria = new AuditMessageCriteria();

            AuditMessage_criteria.mUserId = user_id;

            AuditMessage_list = AuditMessageManager.GetList(AuditMessage_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  AuditMessage_list.Count, auditmessages = AuditMessage_list }, Configuration.Formatters.JsonFormatter);
				
			
		}

		// POST: api/AuditMessage
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(AuditMessage AuditMessage)
		{
			if (AuditMessage.Validate())
			{
				int result;

				result = AuditMessageManager.Save(AuditMessage);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/AuditMessage/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/AuditMessage/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(AuditMessage AuditMessage)
		{
			try
			{
				int result;

				result = AuditMessageManager.Delete(AuditMessage);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}