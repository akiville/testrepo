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
	public class DeliveryController : ApiController
	{
		// GET: api/Delivery
		public HttpResponseMessage Get(int id)
		{
            //try
            //{
                Delivery delivery = new Delivery();
                
                delivery = DeliveryManager.GetItem(id);
                if (delivery != null)
                {
                    DeliveryCollection Delivery_list = new DeliveryCollection();
                    Delivery_list.Add(delivery);

                    return Request.CreateResponse(HttpStatusCode.OK, new { count = Delivery_list.Count, deliverys = Delivery_list }, Configuration.Formatters.JsonFormatter);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
                }
            //}
            //catch
            //{
            //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
            //}
        }

		// GET: api/Delivery/5
		 public HttpResponseMessage Get(int branch_id, String date)
		{
			try
			{
				DeliveryCollection Delivery_list = new DeliveryCollection();
				DeliveryCriteria Delivery_criteria = new DeliveryCriteria();

                Delivery_criteria.mBranchId = branch_id;
                Delivery_criteria.mDeliveryDate = Convert.ToDateTime(date);

                Delivery_list = DeliveryManager.GetList(Delivery_criteria);
				if (Delivery_list.Count > 0 ) 
				{
					return Request.CreateResponse(HttpStatusCode.OK, new { count =  Delivery_list.Count, deliverys = Delivery_list }, Configuration.Formatters.JsonFormatter);
				}
				else
				{
					return Request.CreateResponse(HttpStatusCode.BadRequest, "No record found");
				}
			}
			catch
			{
				return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error" );
			}
		}

		// POST: api/Delivery
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(Delivery Delivery)
		{
			if (Delivery.Validate())
			{
				int result;

				result = DeliveryManager.Save(Delivery);

				return Request.CreateResponse(HttpStatusCode.OK, result); 
			}
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/Delivery/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/Delivery/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(Delivery Delivery)
		{
			try
			{
				int result;

				result = DeliveryManager.Delete(Delivery);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}