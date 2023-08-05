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
	public class RfscMessageController : ApiController
	{
		// GET: api/RfscMessage
		public HttpResponseMessage Get(int id)
		{
            RfscMessageCollection RfscMessage_list = new RfscMessageCollection();
            RfscMessage rfscMessage = new RfscMessage();
            rfscMessage = RfscMessageManager.GetItem(id);
            RfscMessage_list.Add(rfscMessage);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = RfscMessage_list.Count, rfscmessages = RfscMessage_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/RfscMessage/5
        public HttpResponseMessage Get(int to_lmm_id, int lmm_id, String status)
		{
			
			RfscMessageCollection RfscMessage_list = new RfscMessageCollection();
			RfscMessageCriteria RfscMessage_criteria = new RfscMessageCriteria();

            RfscMessage_criteria.mToLmmId = to_lmm_id;
            RfscMessage_criteria.mLmmId = lmm_id;
            RfscMessage_criteria.mStatus = status;

            RfscMessage_list = RfscMessageManager.GetList(RfscMessage_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  RfscMessage_list.Count, rfscmessages = RfscMessage_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/RfscMessage
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(RfscMessage RfscMessage)
		{
			if (RfscMessage.Validate())
			{
				int result;

				result = RfscMessageManager.Save(RfscMessage);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/RfscMessage/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/RfscMessage/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(RfscMessage RfscMessage)
		{
			try
			{
				int result;

				result = RfscMessageManager.Delete(RfscMessage);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}