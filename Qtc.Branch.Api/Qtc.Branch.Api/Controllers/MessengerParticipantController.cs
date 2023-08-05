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
	public class MessengerParticipantController : ApiController
	{
		// GET: api/MessengerParticipant
		public HttpResponseMessage Get(int id)
		{
            MessengerParticipantCollection MessengerParticipant_list = new MessengerParticipantCollection();
            MessengerParticipant messenger_participant = new MessengerParticipant();
            messenger_participant = MessengerParticipantManager.GetItem(id);
            MessengerParticipant_list.Add(messenger_participant);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = MessengerParticipant_list.Count, messengerparticipants = MessengerParticipant_list }, Configuration.Formatters.JsonFormatter);
        }

		// GET: api/MessengerParticipant/5
		 public HttpResponseMessage Get(int messenger_id, int employee_id)
		{
			
			MessengerParticipantCollection MessengerParticipant_list = new MessengerParticipantCollection();
			MessengerParticipantCriteria MessengerParticipant_criteria = new MessengerParticipantCriteria();

            MessengerParticipant_criteria.mMessengerId = messenger_id;
            MessengerParticipant_criteria.mEmployeeId = employee_id;

            MessengerParticipant_list = MessengerParticipantManager.GetList(MessengerParticipant_criteria);
			
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  MessengerParticipant_list.Count, messengerparticipants = MessengerParticipant_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/MessengerParticipant
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(MessengerParticipant MessengerParticipant)
		{
			if (MessengerParticipant.Validate())
			{
				int result;

				result = MessengerParticipantManager.Save(MessengerParticipant);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/MessengerParticipant/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/MessengerParticipant/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(MessengerParticipant MessengerParticipant)
		{
			try
			{
				int result;

				result = MessengerParticipantManager.Delete(MessengerParticipant);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}