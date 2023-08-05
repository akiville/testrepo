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
	public class DeliveryDetailsController : ApiController
	{
		// GET: api/DeliveryDetails
		public HttpResponseMessage Get(int id)
		{
            try
            {
                DeliveryDetails delivery_detail = new DeliveryDetails();
                DeliveryDetailsCollection DeliveryDetails_list = new DeliveryDetailsCollection();

                delivery_detail = DeliveryDetailsManager.GetItem(id);

                DeliveryDetails_list.Add(delivery_detail);


                if (DeliveryDetails_list.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = DeliveryDetails_list.Count, deliverydetails = DeliveryDetails_list }, Configuration.Formatters.JsonFormatter);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { count = 0, deliverydetails = DeliveryDetails_list }, Configuration.Formatters.JsonFormatter);
                }
            }
            catch
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
            }
        }

		// GET: api/DeliveryDetails/5
		 public HttpResponseMessage Get(int dispatch_id, String empty_value)
		{
			try
			{
				DeliveryDetailsCollection DeliveryDetails_list = new DeliveryDetailsCollection();
				DeliveryDetailsCriteria DeliveryDetails_criteria = new DeliveryDetailsCriteria();

                DeliveryDetails_criteria.mDispatchId = dispatch_id;

                DeliveryDetails_list = DeliveryDetailsManager.GetList(DeliveryDetails_criteria);
				if (DeliveryDetails_list.Count > 0 ) 
				{
					return Request.CreateResponse(HttpStatusCode.OK, new { count =  DeliveryDetails_list.Count, deliverydetailss = DeliveryDetails_list }, Configuration.Formatters.JsonFormatter);
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

		// POST: api/DeliveryDetails
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Post(DeliveryDetails DeliveryDetails)
		{
			if (DeliveryDetails.Validate())
			{
				int result;

				result = DeliveryDetailsManager.Save(DeliveryDetails);

                //return Request.CreateResponse(HttpStatusCode.OK, result); 
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
			else
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad request"); 
			}
		}

		// PUT: api/DeliveryDetails/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public void Put(int id, [FromBody]string value)
		{
		}

		// DELETE: api/DeliveryDetails/5
		//[HttpPost]
		//[CustomAuthorizationFilter]
		public HttpResponseMessage Delete(DeliveryDetails DeliveryDetails)
		{
			try
			{
				int result;

				result = DeliveryDetailsManager.Delete(DeliveryDetails);

				return Request.CreateResponse(HttpStatusCode.OK, result);
			}
			catch
			{
			 return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "System Error");
			}
		}
	}
}