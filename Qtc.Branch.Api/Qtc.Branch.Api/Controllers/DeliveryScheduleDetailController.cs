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
	public class DeliveryScheduleDetailController : ApiController
	{
        // GET: api/DeliveryScheduleDetail/5
        public HttpResponseMessage Get(int id)
		{
            DeliveryScheduleDetail delivery_schedule_detail;
            delivery_schedule_detail = DeliveryScheduleDetailManager.GetItem(id);

            return Request.CreateResponse(HttpStatusCode.OK, new { count = 1, deliveryscheduledetails = delivery_schedule_detail }, Configuration.Formatters.JsonFormatter);

        }

        // GET: api/DeliveryScheduleDetail
        public HttpResponseMessage Get(DateTime delivery_date, int branch_id)
        {

            DeliveryScheduleDetailCollection DeliveryScheduleDetail_list = new DeliveryScheduleDetailCollection();
            DeliveryScheduleDetailCriteria DeliveryScheduleDetail_criteria = new DeliveryScheduleDetailCriteria();

            DeliveryScheduleDetail_criteria.mDeliveryDate = delivery_date;
            DeliveryScheduleDetail_criteria.mBranchId = branch_id;

            DeliveryScheduleDetail_list = DeliveryScheduleDetailManager.GetList(DeliveryScheduleDetail_criteria);
           
           return Request.CreateResponse(HttpStatusCode.OK, new { count = DeliveryScheduleDetail_list.Count, deliveryscheduledetails = DeliveryScheduleDetail_list }, Configuration.Formatters.JsonFormatter);
            
        }

		// POST: api/DeliveryScheduleDetail
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(DeliveryScheduleDetail DeliveryScheduleDetail)
		{
			if (DeliveryScheduleDetail.Validate())
			{
				int result;

				result = DeliveryScheduleDetailManager.Save(DeliveryScheduleDetail);

				//return Request.CreateResponse(HttpStatusCode.OK, result); 
				return Request.CreateResponse(HttpStatusCode.OK, new { result = result }, Configuration.Formatters.JsonFormatter); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/DeliveryScheduleDetail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/DeliveryScheduleDetail/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(DeliveryScheduleDetail DeliveryScheduleDetail)
		{
			try
			{
				int result;

				result = DeliveryScheduleDetailManager.Delete(DeliveryScheduleDetail);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}