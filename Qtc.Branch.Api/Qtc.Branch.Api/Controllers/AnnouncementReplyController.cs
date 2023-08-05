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
	public class AnnouncementReplyController : ApiController
	{
		// GET: api/AnnouncementReply
		public HttpResponseMessage Get(int id)
		{
            AnnouncementReply announcement_reply = new AnnouncementReply();
            announcement_reply = AnnouncementReplyManager.GetItem(id);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = 1, announcementreplys = announcement_reply }, Configuration.Formatters.JsonFormatter);
            
        }

        // GET: api/AnnouncementReply/5
        public HttpResponseMessage Get(int employee_id, int announcement_id)
		{
			
			AnnouncementReplyCollection AnnouncementReply_list = new AnnouncementReplyCollection();
			AnnouncementReplyCriteria AnnouncementReply_criteria = new AnnouncementReplyCriteria();

            AnnouncementReply_criteria.mAnnouncementId = announcement_id;
            AnnouncementReply_criteria.mEmployeeId = employee_id;

            AnnouncementReply_list = AnnouncementReplyManager.GetList(AnnouncementReply_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  AnnouncementReply_list.Count, announcementreplys = AnnouncementReply_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/AnnouncementReply
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(AnnouncementReply AnnouncementReply)
		{
			if (AnnouncementReply.Validate())
			{
				int result;

				result = AnnouncementReplyManager.Save(AnnouncementReply);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/AnnouncementReply/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/AnnouncementReply/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(AnnouncementReply AnnouncementReply)
		{
			try
			{
				int result;

				result = AnnouncementReplyManager.Delete(AnnouncementReply);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}