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
	public class DeliveryScheduleConcernController : ApiController
	{
        // GET: api/DeliveryScheduleConcern
        public HttpResponseMessage Get(int id)
        {
            DeliveryScheduleConcernCollection DeliveryScheduleConcern_list = new DeliveryScheduleConcernCollection();
            DeliveryScheduleConcern delivery_schedule_concern = new DeliveryScheduleConcern();
            delivery_schedule_concern = DeliveryScheduleConcernManager.GetItem(id);

            DeliveryScheduleConcern_list.Add(delivery_schedule_concern);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = DeliveryScheduleConcern_list.Count, deliveryscheduleconcerns = DeliveryScheduleConcern_list }, Configuration.Formatters.JsonFormatter);
        }

		// GET: api/DeliveryScheduleConcern/5
		 public HttpResponseMessage Get(int lmm_id, String delivery_date, int delivery_schedule_id )
		{
            DateTime delivery_date_value;

            if (delivery_date == "")
            {
                delivery_date_value = DateTime.MinValue;
            } else
            {
                delivery_date_value = Convert.ToDateTime(delivery_date);
            }
			DeliveryScheduleConcernCollection DeliveryScheduleConcern_list = new DeliveryScheduleConcernCollection();
			DeliveryScheduleConcernCriteria DeliveryScheduleConcern_criteria = new DeliveryScheduleConcernCriteria();

            DeliveryScheduleConcern_criteria.mLmmId = lmm_id;
            DeliveryScheduleConcern_criteria.mDeliveryDate = delivery_date_value;
            DeliveryScheduleConcern_criteria.mDeliveryScheduleId = delivery_schedule_id;

            DeliveryScheduleConcern_list = DeliveryScheduleConcernManager.GetList(DeliveryScheduleConcern_criteria);
				
			return Request.CreateResponse(HttpStatusCode.OK, new { count =  DeliveryScheduleConcern_list.Count, deliveryscheduleconcerns = DeliveryScheduleConcern_list }, Configuration.Formatters.JsonFormatter);
			
		}

		// POST: api/DeliveryScheduleConcern
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(DeliveryScheduleConcern DeliveryScheduleConcern)
		{
			if (DeliveryScheduleConcern.Validate())
			{
				int result;

				result = DeliveryScheduleConcernManager.Save(DeliveryScheduleConcern);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/DeliveryScheduleConcern/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/DeliveryScheduleConcern/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(DeliveryScheduleConcern DeliveryScheduleConcern)
		{
			try
			{
				int result;

				result = DeliveryScheduleConcernManager.Delete(DeliveryScheduleConcern);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}