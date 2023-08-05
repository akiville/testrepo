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
	public class AuditMessageDetailController : ApiController
	{
		// GET: api/AuditMessageDetail
		public HttpResponseMessage Get(int id)
        {
            AuditMessageDetailCollection AuditMessageDetail_list = new AuditMessageDetailCollection();
            AuditMessageDetail audit_message_detail;
            audit_message_detail = AuditMessageDetailManager.GetItem(id);

            AuditMessageDetail_list.Add(audit_message_detail);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = AuditMessageDetail_list.Count, auditmessagedetails = AuditMessageDetail_list }, Configuration.Formatters.JsonFormatter);
        }

		// GET: api/AuditMessageDetail/5
		 public HttpResponseMessage Get(int audit_message_id, int user_id)
		{
			
			AuditMessageDetailCollection AuditMessageDetail_list = new AuditMessageDetailCollection();
			AuditMessageDetailCriteria AuditMessageDetail_criteria = new AuditMessageDetailCriteria();

            AuditMessageDetail_criteria.mAuditMessageId = audit_message_id;
            AuditMessageDetail_criteria.mUserId = user_id;

            AuditMessageDetail_list = AuditMessageDetailManager.GetList(AuditMessageDetail_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  AuditMessageDetail_list.Count, auditmessagedetails = AuditMessageDetail_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/AuditMessageDetail
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(AuditMessageDetail AuditMessageDetail)
		{
			if (AuditMessageDetail.Validate())
			{
				int result;

				result = AuditMessageDetailManager.Save(AuditMessageDetail);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/AuditMessageDetail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/AuditMessageDetail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(AuditMessageDetail AuditMessageDetail)
		{
			try
			{
				int result;

				result = AuditMessageDetailManager.Delete(AuditMessageDetail);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}