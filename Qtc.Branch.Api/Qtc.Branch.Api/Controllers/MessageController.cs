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
	public class MessageController : ApiController
	{
		// GET: api/Message
		public HttpResponseMessage Get(int id)
        {
            MessageCollection Message_list = new MessageCollection();
            Message message = new Message();

            message = MessageManager.GetItem(id);

            Message_list.Add(message);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = Message_list.Count, messages = Message_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/Message/5
        public HttpResponseMessage Get(int receiver_id, int sender_id, int reply_to_id)
		{
			MessageCollection Message_list = new MessageCollection();
			MessageCriteria Message_criteria = new MessageCriteria();

            Message_criteria.mReceiverId = receiver_id;
            Message_criteria.mSenderId = sender_id;
            Message_criteria.mReplyToId = reply_to_id;

            Message_list = MessageManager.GetList(Message_criteria);
			
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  Message_list.Count, messages = Message_list }, Configuration.Formatters.JsonFormatter);
				
			
		}

		// POST: api/Message
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(Message Message)
		{
			if (Message.Validate())
			{
				int result;

				result = MessageManager.Save(Message);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/Message/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/Message/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(Message Message)
		{
			try
			{
				int result;

				result = MessageManager.Delete(Message);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}