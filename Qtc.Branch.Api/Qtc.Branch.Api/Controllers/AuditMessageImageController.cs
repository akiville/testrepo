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
	public class AuditMessageImageController : ApiController
	{
		// GET: api/AuditMessageImage
		public HttpResponseMessage Get(int id)
        {
            AuditMessageImageCollection AuditMessageImage_list = new AuditMessageImageCollection();
            AuditMessageImage audit_message_image;
            audit_message_image = AuditMessageImageManager.GetItem(id);
            AuditMessageImage_list.Add(audit_message_image);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = AuditMessageImage_list.Count, auditmessageimages = AuditMessageImage_list }, Configuration.Formatters.JsonFormatter);
        }

        // GET: api/AuditMessageImage/5
        public HttpResponseMessage Get(int audit_message_detail_id, int user_id)
		{
			
			AuditMessageImageCollection AuditMessageImage_list = new AuditMessageImageCollection();
			AuditMessageImageCriteria AuditMessageImage_criteria = new AuditMessageImageCriteria();

            AuditMessageImage_criteria.mAuditMessageDetailId = audit_message_detail_id;

            AuditMessageImage_list = AuditMessageImageManager.GetList(AuditMessageImage_criteria);
			
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  AuditMessageImage_list.Count, auditmessageimages = AuditMessageImage_list }, Configuration.Formatters.JsonFormatter);
			
				
		}

		// POST: api/AuditMessageImage
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(AuditMessageImage AuditMessageImage)
		{
			if (AuditMessageImage.Validate())
			{
				int result;

				result = AuditMessageImageManager.Save(AuditMessageImage);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/AuditMessageImage/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/AuditMessageImage/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(AuditMessageImage AuditMessageImage)
		{
			try
			{
				int result;

				result = AuditMessageImageManager.Delete(AuditMessageImage);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}