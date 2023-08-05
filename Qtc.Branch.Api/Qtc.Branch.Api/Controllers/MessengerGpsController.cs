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
	public class MessengerGpsController : ApiController
	{
		// GET: api/MessengerGps
		public HttpResponseMessage Get(int id)
		{
            MessengerGps messenger_gps = new MessengerGps();
            MessengerGpsCollection MessengerGps_list = new MessengerGpsCollection();

            messenger_gps = MessengerGpsManager.GetItem(id);
            MessengerGps_list.Add(messenger_gps);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = MessengerGps_list.Count, messengergpss = MessengerGps_list }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/MessengerGps/5
        public HttpResponseMessage Get(int user_id, String type)
		{
			try
			{
				MessengerGpsCollection MessengerGps_list = new MessengerGpsCollection();
				MessengerGpsCriteria MessengerGps_criteria = new MessengerGpsCriteria();

                MessengerGps_criteria.mUserId = user_id;

                MessengerGps_list = MessengerGpsManager.GetList(MessengerGps_criteria);
				return Request.CreateResponse(HttpStatusCode.OK, new { count =  MessengerGps_list.Count, messengergpss = MessengerGps_list }, Configuration.Formatters.JsonFormatter);
				
			}
			catch
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			}
		}

		// POST: api/MessengerGps
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(MessengerGps MessengerGps)
		{
			if (MessengerGps.Validate())
			{
				int result;

				result = MessengerGpsManager.Save(MessengerGps);

				return Request.CreateResponse(HttpStatusCode.OK, result); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/MessengerGps/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/MessengerGps/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(MessengerGps MessengerGps)
		{
			try
			{
				int result;

				result = MessengerGpsManager.Delete(MessengerGps);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}