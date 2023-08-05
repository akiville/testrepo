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
	public class MessengerDetailController : ApiController
	{
		// GET: api/MessengerDetail
		public HttpResponseMessage Get(int id)
        {
            MessengerDetailCollection MessengerDetail_list = new MessengerDetailCollection();
            MessengerDetail messenger_detail = new MessengerDetail();
            messenger_detail = MessengerDetailManager.GetItem(id);
            MessengerDetail_list.Add(messenger_detail);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = MessengerDetail_list.Count, messengerdetails = MessengerDetail_list }, Configuration.Formatters.JsonFormatter);
        }

		// GET: api/MessengerDetail/5
		 public HttpResponseMessage Get(int messenger_id, int employee_id )
		{
			MessengerDetailCollection MessengerDetail_list = new MessengerDetailCollection();
			MessengerDetailCriteria MessengerDetail_criteria = new MessengerDetailCriteria();

            MessengerDetail_criteria.mEmployeeId = employee_id;
            MessengerDetail_criteria.mMessengerId = messenger_id;
            MessengerDetail_list = MessengerDetailManager.GetList(MessengerDetail_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  MessengerDetail_list.Count, messengerdetails = MessengerDetail_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/MessengerDetail
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(MessengerDetail MessengerDetail)
		{
			if (MessengerDetail.Validate())
			{
				int result;

				result = MessengerDetailManager.Save(MessengerDetail);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/MessengerDetail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/MessengerDetail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(MessengerDetail MessengerDetail)
		{
			try
			{
				int result;

				result = MessengerDetailManager.Delete(MessengerDetail);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}