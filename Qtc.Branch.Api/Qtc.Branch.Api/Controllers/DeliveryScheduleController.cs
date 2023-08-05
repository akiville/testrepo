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
	public class DeliveryScheduleController : ApiController
	{
        // GET: api/DeliverySchedule/5
        public HttpResponseMessage Get(int id)
        {
           
            DeliverySchedule delivery_schedule = new DeliverySchedule();

            delivery_schedule = DeliveryScheduleManager.GetItem(id);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = 1, deliveryschedules = delivery_schedule }, Configuration.Formatters.JsonFormatter);
        }

        // GET: api/DeliverySchedule
        public HttpResponseMessage Get(DateTime delivery_start_date, DateTime delivery_end_date, int lmm_id)
		{
			//try
			//{
				DeliveryScheduleCollection DeliverySchedule_list = new DeliveryScheduleCollection();
				DeliveryScheduleCriteria DeliverySchedule_criteria = new DeliveryScheduleCriteria();

                DeliverySchedule_criteria.mLmmId = lmm_id;
                DeliverySchedule_criteria.mDeliveryStartDate = delivery_start_date;
                DeliverySchedule_criteria.mDeliveryEndDate = delivery_end_date;

                DeliverySchedule_list = DeliveryScheduleManager.GetList(DeliverySchedule_criteria);

            
            //if (DeliverySchedule_list.Count > 0 ) 
            //{
            return Request.CreateResponse(HttpStatusCode.OK, new { count =  DeliverySchedule_list.Count, deliveryschedules = DeliverySchedule_list }, Configuration.Formatters.JsonFormatter);
			//	}
			//	else
			//	{
			//		return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
			//	}
			//}
			//catch
			//{
			//	return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			//}
		}

		// POST: api/DeliverySchedule
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(DeliverySchedule DeliverySchedule)
		{
			if (DeliverySchedule.Validate())
			{
				int result;

				result = DeliveryScheduleManager.Save(DeliverySchedule);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/DeliverySchedule/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/DeliverySchedule/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(DeliverySchedule DeliverySchedule)
		{
			try
			{
				int result;

				result = DeliveryScheduleManager.Delete(DeliverySchedule);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}