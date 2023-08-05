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
	public class DeliveryScheduleConcernDetailController : ApiController
	{
		// GET: api/DeliveryScheduleConcernDetail
		public HttpResponseMessage Get(int id)
        {
            DeliveryScheduleConcernDetailCollection DeliveryScheduleConcernDetail_list = new DeliveryScheduleConcernDetailCollection();
            DeliveryScheduleConcernDetail delivery_schedule_concern_detail = new DeliveryScheduleConcernDetail();

            delivery_schedule_concern_detail = DeliveryScheduleConcernDetailManager.GetItem(id);
            DeliveryScheduleConcernDetail_list.Add(delivery_schedule_concern_detail);
            return Request.CreateResponse(HttpStatusCode.OK, new { count = DeliveryScheduleConcernDetail_list.Count, deliveryscheduleconcerndetails = DeliveryScheduleConcernDetail_list }, Configuration.Formatters.JsonFormatter);
        }

        // GET: api/DeliveryScheduleConcernDetail/5
        public HttpResponseMessage Get(int lmm_id, int delivery_schedule_concern_id)
		{
		    DeliveryScheduleConcernDetailCollection DeliveryScheduleConcernDetail_list = new DeliveryScheduleConcernDetailCollection();
		    DeliveryScheduleConcernDetailCriteria DeliveryScheduleConcernDetail_criteria = new DeliveryScheduleConcernDetailCriteria();

            DeliveryScheduleConcernDetail_criteria.mLmmId = lmm_id;
            DeliveryScheduleConcernDetail_criteria.mDeliveryScheduleConcernId = delivery_schedule_concern_id;

            DeliveryScheduleConcernDetail_list = DeliveryScheduleConcernDetailManager.GetList(DeliveryScheduleConcernDetail_criteria);
				
		    return Request.CreateResponse(HttpStatusCode.OK, new { count =  DeliveryScheduleConcernDetail_list.Count, deliveryscheduleconcerndetails = DeliveryScheduleConcernDetail_list }, Configuration.Formatters.JsonFormatter);
				
		}

		// POST: api/DeliveryScheduleConcernDetail
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(DeliveryScheduleConcernDetail DeliveryScheduleConcernDetail)
		{
			if (DeliveryScheduleConcernDetail.Validate())
			{
				int result;

				result = DeliveryScheduleConcernDetailManager.Save(DeliveryScheduleConcernDetail);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/DeliveryScheduleConcernDetail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/DeliveryScheduleConcernDetail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(DeliveryScheduleConcernDetail DeliveryScheduleConcernDetail)
		{
			try
			{
				int result;

				result = DeliveryScheduleConcernDetailManager.Delete(DeliveryScheduleConcernDetail);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}