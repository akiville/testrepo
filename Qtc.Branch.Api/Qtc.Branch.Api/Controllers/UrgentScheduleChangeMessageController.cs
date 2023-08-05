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
	public class UrgentScheduleChangeMessageController : ApiController
	{
		// GET: api/UrgentScheduleChangeMessage
		public HttpResponseMessage Get(int id)
		{
            UrgentScheduleChangeMessageCollection UrgentScheduleChangeMessage_list = new UrgentScheduleChangeMessageCollection();
            UrgentScheduleChangeMessage urgentScheduleChangeMessage;
            urgentScheduleChangeMessage = UrgentScheduleChangeMessageManager.GetItem(id);
            UrgentScheduleChangeMessage_list.Add(urgentScheduleChangeMessage);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = 1, urgentschedulechangemessages = UrgentScheduleChangeMessage_list }, Configuration.Formatters.JsonFormatter);

        }

		// GET: api/UrgentScheduleChangeMessage/5
		 public HttpResponseMessage Get(int to_lmm_id, int lmm_id, String status)
		{
			//try
			//{
				UrgentScheduleChangeMessageCollection UrgentScheduleChangeMessage_list = new UrgentScheduleChangeMessageCollection();
				UrgentScheduleChangeMessageCriteria UrgentScheduleChangeMessage_criteria = new UrgentScheduleChangeMessageCriteria();

                UrgentScheduleChangeMessage_criteria.mToLmmId = to_lmm_id;
                UrgentScheduleChangeMessage_criteria.mLmmId = lmm_id;
                UrgentScheduleChangeMessage_criteria.mStatus = status;

                UrgentScheduleChangeMessage_list = UrgentScheduleChangeMessageManager.GetList(UrgentScheduleChangeMessage_criteria);
				
				return Request.CreateResponse(HttpStatusCode.OK, new { count =  UrgentScheduleChangeMessage_list.Count, urgentschedulechangemessages = UrgentScheduleChangeMessage_list }, Configuration.Formatters.JsonFormatter);
				
		}

        public HttpResponseMessage Get(int urgent_schedule_change_id, String dummy)
        {
            //try
            //{
            UrgentScheduleChangeMessageCollection UrgentScheduleChangeMessage_list = new UrgentScheduleChangeMessageCollection();
            UrgentScheduleChangeMessageCriteria UrgentScheduleChangeMessage_criteria = new UrgentScheduleChangeMessageCriteria();

            UrgentScheduleChangeMessage_criteria.mUrgentScheduleChangeId = urgent_schedule_change_id;

            UrgentScheduleChangeMessage_list = UrgentScheduleChangeMessageManager.GetList(UrgentScheduleChangeMessage_criteria);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = UrgentScheduleChangeMessage_list.Count, urgentschedulechangemessages = UrgentScheduleChangeMessage_list }, Configuration.Formatters.JsonFormatter);

        }

        // POST: api/UrgentScheduleChangeMessage
        //[HttpPost]
        //[CustomAuthorizationFilter]
        public HttpResponseMessage Post(UrgentScheduleChangeMessage UrgentScheduleChangeMessage)
		{
			if (UrgentScheduleChangeMessage.Validate())
			{
				int result;

				result = UrgentScheduleChangeMessageManager.Save(UrgentScheduleChangeMessage);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/UrgentScheduleChangeMessage/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/UrgentScheduleChangeMessage/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(UrgentScheduleChangeMessage UrgentScheduleChangeMessage)
		{
			try
			{
				int result;

				result = UrgentScheduleChangeMessageManager.Delete(UrgentScheduleChangeMessage);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}