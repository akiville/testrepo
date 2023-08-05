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
	public class MessengerController : ApiController
	{
		// GET: api/Messenger
		public HttpResponseMessage Get(int id)
		{
            MessengerCollection Messenger_list = new MessengerCollection();
            Messenger messenger = new Messenger();
            messenger = MessengerManager.GetItem(id);
            Messenger_list.Add(messenger);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = Messenger_list.Count, messengers = Messenger_list }, Configuration.Formatters.JsonFormatter);
            
        }

        // GET: api/Messenger/5
        public HttpResponseMessage Get(String title, int employee_id)
		{
			
			MessengerCollection Messenger_list = new MessengerCollection();
			MessengerCriteria Messenger_criteria = new MessengerCriteria();

            Messenger_criteria.mEmployeeId = employee_id;
            Messenger_criteria.mTitle = title;
            Messenger_list = MessengerManager.GetList(Messenger_criteria);
			
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  Messenger_list.Count, messengers = Messenger_list }, Configuration.Formatters.JsonFormatter);
			
		}

		// POST: api/Messenger
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(Messenger Messenger)
		{
			if (Messenger.Validate())
			{
				int result;

				result = MessengerManager.Save(Messenger);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/Messenger/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/Messenger/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(Messenger Messenger)
		{
			try
			{
				int result;

				result = MessengerManager.Delete(Messenger);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}