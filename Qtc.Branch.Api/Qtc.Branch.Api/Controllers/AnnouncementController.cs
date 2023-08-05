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
	public class AnnouncementController : ApiController
	{
		// GET: api/Announcement
		public HttpResponseMessage Get(int id)
		{
            Announcement announcement = new Announcement();
            announcement = AnnouncementManager.GetItem(id);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = 1, announcements = announcement }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/Announcement/5
        public HttpResponseMessage Get()
		{
			
				AnnouncementCollection Announcement_list = new AnnouncementCollection();
				AnnouncementCriteria Announcement_criteria = new AnnouncementCriteria();

				Announcement_list = AnnouncementManager.GetList(Announcement_criteria);
				
				return Request.CreateResponse(HttpStatusCode.OK, new { count =  Announcement_list.Count, announcements = Announcement_list }, Configuration.Formatters.JsonFormatter);
				
			
		}

		// POST: api/Announcement
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(Announcement Announcement)
		{
			if (Announcement.Validate())
			{
				int result;

				result = AnnouncementManager.Save(Announcement);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/Announcement/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/Announcement/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(Announcement Announcement)
		{
			try
			{
				int result;

				result = AnnouncementManager.Delete(Announcement);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}